using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace PlaniIt
{
    public class PlantIt_Jupiter
    {
        private readonly ILogger<PlantIt_Jupiter> _logger;

        public PlantIt_Jupiter(ILogger<PlantIt_Jupiter> logger)
        {
            _logger = logger;
        }

        [Function("PlantIt_Jupiter")]
        public IActionResult Run([HttpTrigger(AuthorizationLevel.Function, "get", "post")] HttpRequest req)
        {
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            return new OkObjectResult("Welcome to Azure Functions!");
        }
    }
}
