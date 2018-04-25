using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Model;
using tNext.Common.Model;
using tNext.Microservices.Configuration.Api.Services;

namespace tNext.Microservices.Configuration.Api.Controllers
{
    [RoutePrefix("Values")]
    public class ValuesController : ApiController
    {
        tNextConfigurationService _tNextconfService = new tNextConfigurationService();

        List<ConfigurationItem> GetConfigurationFromDB(string applicationName, string environment)
        {
            return _tNextconfService.GetConfigurationFromDB(applicationName, environment);
        }

        [HttpGet]
        public HttpResponseMessage Get([FromUri]GetConfigurationRequest request)
        {
            var result = GetConfigurationFromDB(request.ApplicationName, ConfigurationManager.AppSettings["Environment"]);
            return tNextResponseCreator.OK(result);
        }
    }
}
