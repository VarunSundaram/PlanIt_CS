using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.IO;

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

            DirectoryInfo directory = new DirectoryInfo(Directory.GetCurrentDirectory());

            if (directory != null && directory.GetFiles("PlanIT_Test.dll").Any())
            {
                _logger.LogInformation("Executable file is not found");
            }
            else
            {
                while (directory != null && !directory.GetFiles("PlanIT_Test.dll").Any())
                {
                    directory = directory.Parent;
                }
            }
            if (directory == null)
            {
                _logger.LogInformation("Execution folder not found");
                return new OkObjectResult("Execution Abandoned! " + Directory.GetCurrentDirectory().ToString());
            }

            // Create a new Process object.
            Process process = new Process();
            process.StartInfo.FileName = "cmd.exe";
            process.StartInfo.Arguments = "dotnet nunit ./output/PlanIT_Test.dll";
            process.StartInfo.WorkingDirectory = directory.Name;
            // Start the process.
            process.Start();
            process.WaitForExit();

            // Read the output of the process.
            string output = process.StandardOutput.ReadToEnd();
            _logger.LogInformation(output);
            return new OkObjectResult("End of Execution with " + output);
        }
    }
}
