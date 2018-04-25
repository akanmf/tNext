using System.Net.Http;
using System.Threading.Tasks;
using tNext.Common.Model.Errors;

namespace tNext.ApiGateway.Api
{
    public class Utils
    {
        public static async Task<tNextMicroserviceCallError> GetTnextErrorFromHttpResponseMessage(HttpResponseMessage originalResponse)
        {
            var responseString = await originalResponse.Content.ReadAsStringAsync();
            var tnextCallError = new tNextMicroserviceCallError(code: originalResponse.StatusCode.ToString(), internalMessage: responseString, externalMessage: "Hata oluştu");
            tnextCallError.MaskForClientApplications();

            return tnextCallError;
        }
    }
}