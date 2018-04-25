using System.Configuration;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using tNext.Common.Core;

namespace tNext.ApiGateway.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            if (string.Compare(ConfigurationManager.AppSettings["UnderConstruction"] ?? "", "true", true) != 0)
            {
                tNextMicroservice.Start();
            }
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
    }
}
