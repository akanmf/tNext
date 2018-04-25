using System.Web.Http;
using tNext.Common.Core;

namespace tNext.Microservices.Order.Api
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
                defaults: new { Controller="Order", id = RouteParameter.Optional }
            );
            tNextMicroservice.Start();
        }
    }
}
