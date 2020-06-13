using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebForm
{
    public class MvcApplication : System.Web.HttpApplication
    {
        // Set httpclient with a base Url here
        public static HttpClient httpClient = new HttpClient() { BaseAddress = new Uri(WebConfigurationManager.AppSettings["apiBaseAddress"]) };

        protected void Application_Start()
        {
            // Set access token for client
            var ACCESS_TOKEN = WebConfigurationManager.AppSettings["access_token"];
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ACCESS_TOKEN);

            ServicePointManager.SecurityProtocol = SecurityProtocolType.Ssl3 |
                                        SecurityProtocolType.Tls |
                                        SecurityProtocolType.Tls11 |
                                        SecurityProtocolType.Tls12;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
