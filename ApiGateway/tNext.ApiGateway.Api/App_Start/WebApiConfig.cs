using System.Configuration;
using System.Web.Http;
using tNext.ApiGateway.Api.Handlers;

namespace tNext.ApiGateway.Api
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.MessageHandlers.Add(new CommonMessageHandler());

            if (string.Compare(ConfigurationManager.AppSettings["UnderConstruction"] ?? "", "true", true) != 0)
            {
                // Web API routes
                config.MapHttpAttributeRoutes();
                config.Routes.MapHttpRoute(
                    name: "Microservices",
                    routeTemplate: "api/{microservice}/{*urlParts}",
                    defaults: new { microservice = "" },
                    handler: new tNextMicroservicesHandler(),
                    constraints: null
                );

                config.Routes.MapHttpRoute(
                    name: "TeknosaServices",
                    routeTemplate: "TeknosaServices/{*urlParts}",
                    defaults: new { controller = "Relay", action = "Route" },
                    handler: new TeknosaServicesHandler(),
                    constraints: null
                );
            }
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "{*url}",
                defaults: new { }
            );

        }
    }
}
