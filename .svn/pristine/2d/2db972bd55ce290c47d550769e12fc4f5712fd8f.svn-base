using System;
using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Filters;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;

namespace tNext.ApiGateway.Api.Controllers
{
    [RoutePrefix("Admin")]
    public class AdminController : ApiController
    {

        [Route("Routes")]
        [HttpGet]
        public HttpResponseMessage Routes()
        {
            return tNextResponseCreator.OK(tNext.ApiGateway.Api.Handlers.tNextMicroservicesHandler.routes);
        }

        [Route("Ping")]
        [HttpGet]
        public HttpResponseMessage Ping()
        {
            return tNextResponseCreator.OK("Pong");
        }

        [Route("ClearCaches")]
        [HttpGet]
        public HttpResponseMessage ClearCaches()
        {
            var localCache = CacheHelper.Local.ClearAllCachedItems();
            var remoteCache = CacheHelper.Remote.ClearAllCachedItems();
            var response = new
            {
                LacalCache = localCache,
                RemoteCache = remoteCache
            };
            return tNextResponseCreator.OK(response);
        }
    }
}