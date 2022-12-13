using System.Security.Cryptography;
using System.Text;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionRSATests.API;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var rsa = Base64ToRSA(Environment.GetEnvironmentVariable("RSA_KEY") ?? throw new InvalidOperationException("RSA_KEY is not set"));
        builder.Services.AddSingleton(rsa);
    }

    private static RSA Base64ToRSA(string base64)
    {
        var bytes = Convert.FromBase64String(base64);
        var rsa = RSA.Create();
        rsa.ImportFromPem(Encoding.UTF8.GetChars(bytes));
        return rsa;
    }
}