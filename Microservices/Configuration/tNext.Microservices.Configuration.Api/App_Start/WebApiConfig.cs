using System.Web.Http;
using tNext.Common.Core.Handlers;

namespace tNext.Microservices.Configuration.Api
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
                defaults: new { controller = "values", id = RouteParameter.Optional }
            );

            //GlobalConfiguration.Configuration.MessageHandlers.Add(new LoggingMessageHandler());
        }
    }
}
