using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using StuntmanFunctionApp.Services;

namespace StuntmanFunctionApp
{
    public static class Function1
    {
        [FunctionName("Stuntman")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = "stuntman")] HttpRequest req,
            ILogger log)
        {
            var stuntmanService = new StuntmanService();
            var companyName = stuntmanService.GetRandomCompany();
            var stuntman = stuntmanService.CreateStuntman(100, companyName, "enyoi", "local", "en");
     
            var data = JsonConvert.SerializeObject(stuntman);

            return new OkObjectResult(data);
        }
    }
}
