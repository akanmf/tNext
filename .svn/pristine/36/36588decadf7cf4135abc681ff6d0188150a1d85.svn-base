using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Filters;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;
using tNext.Microservices.Environment.Api.Service;

namespace tNext.Microservices.Environment.Api.Controllers
{
    public class EnvironmentController : ApiController
    {
        EnvironmentService environmentService = new EnvironmentService();

        [Route("ordertypes/{orderType}")]
        [HttpGet]
        [Cache(cacheType: CacheType.Local, clientCaching: true, cacheDuration: 3600)]
        public HttpResponseMessage OrderTypes(OrderTypePages orderType)
        {
            var resp = environmentService.GetOrderTypes(orderType);
            return tNextResponseCreator.OK(resp);
        }
    }
}