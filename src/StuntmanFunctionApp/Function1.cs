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
using StuntmanFunctionApp.Models;
using System.Collections.Generic;

namespace StuntmanFunctionApp
{
    public static class Function1
    {
        [FunctionName("Stuntman")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Function, "post", Route = "stuntman")] HttpRequest req,
            ILogger log)
        { 
            List<object> resultList = new List<object>();
            List<object> stuntman = new List<object>();
            List<object> departments = new List<object>();
            resultList.Add(stuntman);
            resultList.Add(departments);

            //Read Request Body
            var content = await new StreamReader(req.Body).ReadToEndAsync();
            var body = JsonConvert.DeserializeObject<UpSertModel>(content);
            
            var stuntmanService = new StuntmanService();
            var departmentService = new DepartmentService();
            var generatedStuntman = stuntmanService.CreateStuntman(body.Amount, body.CompanyName, body.DomainName, body.DomainSuffix, body.Locale);
            var generatedDepartments = departmentService.CreateDepartmentsAndAssignManager(generatedStuntman);

            stuntman.AddRange(generatedStuntman);
            departments.AddRange(generatedDepartments);

            var returnData = JsonConvert.SerializeObject(resultList);

            return new OkObjectResult(returnData);
        }
    }
}
