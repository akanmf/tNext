using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Filters;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;
using tNext.Microservices.Parameter.Api.Service;

namespace tNext.Microservices.Parameter.Api.Controllers
{
    public class ParameterController : ApiController
    {
        ParameterService parameterService = new ParameterService();

        [HttpGet]
        [Cache(cacheType: CacheType.Local, clientCaching: true, cacheDuration: 3600)]
        public HttpResponseMessage Get(string group = "", string key = "")
        {
            var result = parameterService.GetParameters(group, key);
            return tNextResponseCreator.OK(result);
        }
    }
}
