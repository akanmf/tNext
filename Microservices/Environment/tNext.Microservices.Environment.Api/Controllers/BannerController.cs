using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Filters;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;
using tNext.Microservices.Environment.Api.Enums;
using tNext.Microservices.Environment.Api.Service;

namespace tNext.Microservices.Environment.Api.Controllers
{
    [RoutePrefix("Banner")]
    public class BannerController : ApiController
    {
        BannerService bannerService = new BannerService();

        [Route("Advertisement/{bannerCategory}/{applicationType}")]
        [Cache(cacheType: CacheType.Local, clientCaching: true, cacheDuration: 300)]
        public HttpResponseMessage GetAdvertisementsByTypeForBannerWithArea(BannerCategory bannerCategory, ApplicationType applicationType)
        {
            var result = bannerService.GetAdvertisementsByTypeForBannerWithArea(bannerCategory, applicationType);

            return tNextResponseCreator.OK(result);
        }
    }
}
