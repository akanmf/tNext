using System.Net.Http;
using System.Web.Http;
using System.Web.Routing;
using tNext.Common.Core.Model;
using tNext.Microservices.Environment.Api.Service;

namespace tNext.Microservices.Environment.Api.Controllers
{
    [RoutePrefix("agreements")]
    public class AgreementsController : ApiController
    {
        AgreementsService _agreementsservice = new AgreementsService();
        [Route("register")]
        [HttpGet]
        public HttpResponseMessage GetConfidentialityAgreements()
        {
            var response = _agreementsservice.GetConfidentialityAgreement();
            return tNextResponseCreator.OK(response);
        }

    }
}