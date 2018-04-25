using System.Web.Mvc;
using System.Web.Routing;

namespace tNext.ApiGateway.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
        }
    }
}
