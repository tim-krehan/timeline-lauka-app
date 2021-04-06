using System;
using System.Collections;
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
    public static class Timeline
    {
        [FunctionName("timeline")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "timeline")] HttpRequest req, ExecutionContext context)
        {
            string content = await System.IO.File.ReadAllTextAsync(Path.Combine(context.FunctionDirectory, "../static/index.html"), System.Text.Encoding.UTF8);

            TableDatabase db = new TableDatabase(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "timeline");
            IEnumerable allitems = db.GetAllItems();

            string htmlitems = "";
            foreach (TimelineItem item in allitems)
            {
                htmlitems += "<li>\n";
                htmlitems += $"    <div class='time'>{item.TimeText}</div>\n";
                htmlitems += $"    <p>{item.Text}</p>\n";
                htmlitems += "</li>\n";
            }

            content = content.Replace("##ITEMS##", htmlitems);
            content = content.Replace("##TODAY##", DateTime.Now.ToString("dd.MM.yyyy"));


            return new ContentResult { Content = content, ContentType = "text/html", StatusCode = 200 };
        }
    }
}

