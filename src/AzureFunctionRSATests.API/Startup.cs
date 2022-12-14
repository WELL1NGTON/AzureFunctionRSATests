using System.Security.Cryptography;
using System.Text;

using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

[assembly: FunctionsStartup(typeof(AzureFunctionRSATests.API.Startup))]
namespace AzureFunctionRSATests.API;

public class Startup : FunctionsStartup
{
    public override void Configure(IFunctionsHostBuilder builder)
    {
        var rsa = Base64ToRSA(Environment.GetEnvironmentVariable("RSA_KEY_BASE64") ?? throw new InvalidOperationException("RSA_KEY_BASE64 is not set"));
        builder.Services.AddSingleton(new TestClassWithRSA { RSA = rsa });
    }

    private static RSA Base64ToRSA(string base64)
    {
        var bytes = Convert.FromBase64String(base64);
        var rsa = RSA.Create();
        rsa.ImportFromPem(Encoding.UTF8.GetChars(bytes));
        return rsa;
    }
}