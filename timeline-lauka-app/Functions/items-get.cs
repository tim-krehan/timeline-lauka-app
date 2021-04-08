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
    public static class ItemsGetAll
    {
        [FunctionName("items-get-all")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items")] HttpRequest req)
        {
            string apikey = req.Headers["x-api-key"];
            if (string.IsNullOrEmpty(apikey))
                return new UnauthorizedResult();
            if (apikey != Environment.GetEnvironmentVariable("APP_USER_APIKEY"))
                return new UnauthorizedResult();

            TableDatabase db = new TableDatabase(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "timeline");

            List<TimelineItem> allitems = db.GetAllItems().ToList();

            return new ContentResult { Content = JsonConvert.SerializeObject(allitems), ContentType = "application/json", StatusCode = 200 };
        } 
    }
    public static class ItemsGet
    {
        [FunctionName("items-get")]
        public static async Task<IActionResult> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "items/{itemid}")] HttpRequest req, string itemid)
        {
            string apikey = req.Headers["x-api-key"];
            if (string.IsNullOrEmpty(apikey))
                return new UnauthorizedResult();
            if (apikey != Environment.GetEnvironmentVariable("APP_USER_APIKEY"))
                return new UnauthorizedResult();

            TableDatabase db = new TableDatabase(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "timeline");

            TimelineItem item = db.GetItemByKey(itemid);

            if (item == null)
                return new NotFoundResult();

            return new ContentResult { Content = JsonConvert.SerializeObject(item), ContentType = "application/json", StatusCode = 200 };
        }
    }
    
}
