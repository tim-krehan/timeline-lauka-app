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
    public static class ItemPost
    {
        [FunctionName("item-post")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "item")] HttpRequest req)
        {
            string apikey = req.Headers["x-api-key"];
            if (string.IsNullOrEmpty(apikey))
                return new UnauthorizedResult();
            if (apikey != Environment.GetEnvironmentVariable("APP_USER_APIKEY"))
                return new UnauthorizedResult();

            StreamReader reader = new StreamReader(req.Body);
            string request = reader.ReadToEnd();

            TimelineItem requesteditem = JsonConvert.DeserializeObject<TimelineItem>(request);

            if (requesteditem == null)
                return new BadRequestResult();
            if ((requesteditem.Text == null) || (requesteditem.TimeText == null) || (requesteditem.OrderByTime == null))
                return new BadRequestResult();

            TableDatabase db = new TableDatabase(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "timeline");
            

            string newguid = Guid.NewGuid().ToString();
            requesteditem.RowKey = newguid;
            requesteditem.PartitionKey = newguid;

            await db.AddItemAsync(requesteditem);

            return new ContentResult { Content = JsonConvert.SerializeObject(requesteditem), ContentType = "application/json", StatusCode = 200 };
        }
    }
}

