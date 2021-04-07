using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace timeline_lauka_app
{
    public static class ItemGet
    {
        [FunctionName("item-get")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "item")] HttpRequest req)
        {
            string apikey = req.Headers["x-api-key"];
            if (string.IsNullOrEmpty(apikey))
                return new UnauthorizedResult();
            if (apikey != Environment.GetEnvironmentVariable("APP_USER_APIKEY"))
                return new UnauthorizedResult();

            TableDatabase db = new TableDatabase(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "timeline");

            List<TimelineItem> alldomains = db.GetAllItems().ToList();

            return new ContentResult { Content = JsonConvert.SerializeObject(alldomains), ContentType = "application/json", StatusCode = 200 };
        }
    }
}

