using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace PlanIt.Task
{
    public class PlanIT_Trigger
    {
        private readonly ILogger<PlanIT_Trigger> _logger;

        public PlanIT_Trigger(ILogger<PlanIT_Trigger> logger)
        {
            _logger = logger;
        }

        [Function("PlanIT_Trigger")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            // # dotnet nunit ./output/PlanIT_Test.dll 
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
