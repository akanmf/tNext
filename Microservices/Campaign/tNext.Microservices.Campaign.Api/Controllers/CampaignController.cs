using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Filters;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;
using tNext.Microservices.Campaign.Api.Services;

namespace tNext.Microservices.Campaign.Api.Controllers
{
    public class CampaignController : ApiController
    {
        CampaignService _campaignService = new CampaignService();

        [Route("")]
        [HttpGet]
        [Cache(cacheType: CacheType.Local, clientCaching: true, cacheDuration: 300)]
        public HttpResponseMessage Get()
        {
            var result = _campaignService.GetCampaigns(true, true);
            return tNextResponseCreator.OK(result);
        }

        [Route("{campaignMasterId}")]
        [HttpGet]
        public HttpResponseMessage Get(int campaignMasterId)
        {
            var campaignMaster = _campaignService.GetCampaignMaster(campaignMasterId);

            var result = new
            {
                Id = campaignMaster.Id,
                PairId = campaignMaster.PairId,
                Name = campaignMaster.Name,
                Description = "<![CDATA[" + campaignMaster.Description + "]]>",
                HtmlContent = "<![CDATA[" + campaignMaster.HtmlContent + "]]>",
                Image = (!string.IsNullOrEmpty(campaignMaster.Image) && campaignMaster.Image.StartsWith("//")) ? campaignMaster.Image.Replace("//", "http://") : campaignMaster.Image,
                CampaignStartDate = campaignMaster.StartDate,
                CampaignEndDate = campaignMaster.EndDate,
                Link = campaignMaster.CampaignLink
            };

            return tNextResponseCreator.OK(result);
        }

    }
}
