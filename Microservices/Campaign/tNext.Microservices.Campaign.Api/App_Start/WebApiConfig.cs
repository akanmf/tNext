using System.Web.Http;
using tNext.Common.Core;

namespace tNext.Microservices.Campaign.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{controller}/{id}",
                defaults: new { controller ="Campaign", id = RouteParameter.Optional }
            );

            tNextMicroservice.Start();
        }
    }
}
