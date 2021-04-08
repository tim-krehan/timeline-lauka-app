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
        //[FunctionName("items-put")]
        //public static async Task<IActionResult> Run(
        //    [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "items/{itemid}")] HttpRequest req, string itemid)
        //{
        //    string apikey = req.Headers["x-api-key"];
        //    if (string.IsNullOrEmpty(apikey))
        //        return new UnauthorizedResult();
        //    if (apikey != Environment.GetEnvironmentVariable("APP_USER_APIKEY"))
        //        return new UnauthorizedResult();
        //
        //    StreamReader sr = new StreamReader(req.Body);
        //    string requestbody = sr.ReadToEnd();
        //
        //    DynDnsRESTRequestBody request;
        //
        //    try
        //    {
        //        request = JsonConvert.DeserializeObject<DynDnsRESTRequestBody>(requestbody);
        //    }
        //    catch
        //    {
        //        return new BadRequestResult();
        //    }
        //
        //    if (string.IsNullOrWhiteSpace(domainname))
        //    {
        //        return new BadRequestResult();
        //    }
        //
        //    TableDatabase db = new TableDatabase(Environment.GetEnvironmentVariable("AzureWebJobsStorage"), "domains");
        //    Logger logger = new Logger(db.GetTable("logging"));
        //    Domain domainentry = db.GetDomainByKey(apikey);
        //
        //    if (domainentry.DomainName != domainname)
        //    {
        //        return new BadRequestResult();
        //    }
        //
        //
        //    IPAddress requestv4;
        //    IPAddress requestv6;
        //
        //    bool ipv4valid = IPAddress.TryParse(request.IPv4, out requestv4);
        //    bool ipv6valid = IPAddress.TryParse(request.IPv6, out requestv6);
        //
        //    if (!ipv4valid && !ipv6valid)
        //    {
        //        return new BadRequestResult();
        //    }
        //
        //    if (!string.IsNullOrEmpty(request.IPv4) && !ipv4valid)
        //        return new BadRequestResult();
        //    if (!string.IsNullOrEmpty(request.IPv6) && !ipv6valid)
        //        return new BadRequestResult();
        //    if (requestv4.AddressFamily != System.Net.Sockets.AddressFamily.InterNetwork)
        //        return new BadRequestResult();
        //    if (requestv6.AddressFamily != System.Net.Sockets.AddressFamily.InterNetworkV6)
        //        return new BadRequestResult();
        //
        //    FQDN dnsname;
        //    try
        //    {
        //        dnsname = new FQDN(domainentry.DomainName);
        //    }
        //    catch (ArgumentException)
        //    {
        //        return new BadRequestResult();
        //    }
        //
        //    DNSManager dnsmanager = new DNSManager();
        //    dnsmanager.TenantID = Environment.GetEnvironmentVariable("APP_SECRET_TENANTID");
        //    dnsmanager.ClientID = Environment.GetEnvironmentVariable("APP_SECRET_CLIENTID");
        //    dnsmanager.SubscriptionID = Environment.GetEnvironmentVariable("APP_SECRET_SUBSCRIPTIONID");
        //    dnsmanager.Secret = Environment.GetEnvironmentVariable("APP_SECRET_SECRET");
        //    dnsmanager.ResourceGroup = Environment.GetEnvironmentVariable("APP_SECRET_RESOURCEGROUP");
        //
        //    await dnsmanager.LoginAsync();
        //    if (requestv4 != null)
        //    {
        //        await dnsmanager.UpdateIPRecordAsync(dnsname, requestv4);
        //
        //        logger.Log(dnsname.FullDomain, requestv4.ToString());
        //    }
        //
        //    if (requestv6 != null)
        //    {
        //        await dnsmanager.UpdateIPRecordAsync(dnsname, requestv6);
        //
        //        logger.Log(dnsname.FullDomain, requestv6.ToString());
        //    }
        //
        //    return new OkResult();
        //}
    }
}

