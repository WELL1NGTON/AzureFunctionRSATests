using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Microsoft.Extensions.DependencyInjection;

namespace AzureFunctionRSATests.API
{
    public static class TestRsaGet
    {
        [FunctionName("TestRsaGet")]
        public static Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            var rsaClass = req.HttpContext.RequestServices.GetRequiredService<TestClassWithRSA>();

            var responseMessage = $"Your function executed successfully. RSA: {rsaClass.RSA.ToXmlString(true)}";

            return Task.FromResult(new OkObjectResult(responseMessage) as IActionResult);
        }
    }
}
