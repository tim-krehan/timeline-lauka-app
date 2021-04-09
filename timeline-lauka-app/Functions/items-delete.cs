using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace timeline_lauka_app
{
    public static class ItemsDelete
    {
        [FunctionName("items-delete")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "items/{itemid}")] HttpRequest req, string itemid)
        {
            string apikey = req.Headers["x-api-key"];
            if (string.IsNullOrEmpty(apikey))
                return new UnauthorizedResult();
            if (apikey != Environment.GetEnvironmentVariable("APP_USER_APIKEY"))
                return new UnauthorizedResult();

            TableDatabase db = new TableDatabase(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "timeline");
            TimelineItem item = db.GetItemByKey(itemid);

            if (item != null)
            {
                await db.DeleteItemAsync(item);
                return new NoContentResult();
            }
            else
            {
                return new NotFoundResult();
            }
        }
    }
}

