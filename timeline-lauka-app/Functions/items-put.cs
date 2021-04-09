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
    public static class ItemsPut
    {
        [FunctionName("items-put")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "items/{itemid}")] HttpRequest req, string itemid)
        {
            string apikey = req.Headers["x-api-key"];
            if (string.IsNullOrEmpty(apikey))
                return new UnauthorizedResult();
            if (apikey != Environment.GetEnvironmentVariable("APP_USER_APIKEY"))
                return new UnauthorizedResult();


            StreamReader sr = new StreamReader(req.Body);
            string requestbody = sr.ReadToEnd();
        
            TimelineItem requesteditem;
        
            try
            {
                requesteditem = JsonConvert.DeserializeObject<TimelineItem>(requestbody);
            }
            catch
            {
                return new BadRequestResult();
            }

            requesteditem.RowKey = itemid;
            requesteditem.PartitionKey = itemid;

            TableDatabase db = new TableDatabase(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "timeline");
            TimelineItem existingitem = db.GetItemByKey(itemid);

            var test = 22;
        
            if (existingitem == null)
            {
                await db.AddItemAsync(requesteditem);
                return new ContentResult { Content = null, StatusCode = 201 };
            }
            else
            {
                requesteditem.ETag = existingitem.ETag;
                await db.ReplaceItemAsync(requesteditem);
                return new NoContentResult();
            }
        }
    }
}

