#!/usr/bin/env dotnet-script

#r "nuget: CliWrap, 3.5.0"

using Internal;
using CliWrap;
using System.Reflection;
using System.Text.RegularExpressions;

WriteLine("Generating private key...");

// Gambiarra, essa forma de pegar o path do projeto não sei se vai funcionar sempre...
var projectPath = Environment.GetCommandLineArgs()[1];
projectPath = Directory.GetParent(projectPath).FullName;

await Cli.Wrap("openssl")
    .WithArguments(new[]
    {
        "genrsa",
        "-out", Path.Combine(projectPath,"private-key.pem"),
        "3072"
    })
    .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
    .ExecuteAsync();

// openssl rsa -in private-key.pem -pubout -out public-key.pem

WriteLine("Generating public key...");

await Cli.Wrap("openssl")
    .WithArguments(new[]
    {
        "rsa",
        "-in",  Path.Combine(projectPath, "private-key.pem"),
        "-pubout",
        "-out",  Path.Combine(projectPath, "public-key.pem")
    })
    .WithStandardOutputPipe(PipeTarget.ToDelegate(Console.WriteLine))
    .ExecuteAsync();

WriteLine("Converting private key to base64...");

var privateKey = File.ReadAllText(Path.Combine(projectPath, "private-key.pem"));

var privateKeyBytes = Encoding.UTF8.GetBytes(privateKey);

var privateKeyBase64 = Convert.ToBase64String(privateKeyBytes);

File.WriteAllText(Path.Combine(projectPath, "src", "AzureFunctionRSATests.API", "private-key-base64.txt"), privateKeyBase64);

// Read all text from ./src/local.settings.sample.json

var localSettingsSample = File.ReadAllText(Path.Combine(projectPath, "src", "AzureFunctionRSATests.API", "local.settings.sample.json"));

// Replace "RSA_KEY_BASE64" value with the generated base64

localSettingsSample = Regex.Replace(localSettingsSample, "\"RSA_KEY_BASE64\": \"(.*)\"", $"\"RSA_KEY_BASE64\": \"{privateKeyBase64}\"");

// Write the new local.settings.json

File.WriteAllText(Path.Combine(projectPath, "src", "AzureFunctionRSATests.API", "local.settings.json"), localSettingsSample);
