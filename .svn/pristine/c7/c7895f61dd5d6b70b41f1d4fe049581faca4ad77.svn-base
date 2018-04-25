using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Model;
using tNext.Microservices.Configuration.Api.Services;

namespace tNext.Microservices.Configuration.Api.Controllers
{
    [RoutePrefix("TeknosaConfiguration")]
    public class TeknosaConfigurationController : ApiController
    {
        TeknosaConfigurationService _teknosaConfigurationService = new TeknosaConfigurationService();

        [Route("{code}")]
        [HttpGet]
        public HttpResponseMessage Get([FromUri]string code)
        {
            var result = _teknosaConfigurationService.GetConfigurationByCode(code);
            return tNextResponseCreator.OK(result);
        }
    }
}