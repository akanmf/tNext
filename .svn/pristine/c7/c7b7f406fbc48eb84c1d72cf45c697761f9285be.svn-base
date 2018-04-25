using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using tNext.ApiGateway.Api.Helpers;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model.Errors;

namespace tNext.ApiGateway.Api.Handlers
{
    public class tNextMicroservicesHandler : DelegatingHandler
    {

        public static Dictionary<string, string> routes = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        static tNextMicroservicesHandler()
        {
            //TODO: route bilgilerinide configden almamız gerekiyor.
            routes.Add("Campaign", ConfigurationHelper.GetConfiguration("tNext.Microservices.Campaign.Api.Url"));
            routes.Add("Customer", ConfigurationHelper.GetConfiguration("tNext.Microservices.Customer.Api.Url"));
            routes.Add("Configuration", ConfigurationHelper.GetConfiguration("tNext.Microservices.Configuration.Api.Url"));
            routes.Add("Parameter", ConfigurationHelper.GetConfiguration("tNext.Microservices.Parameter.Api.Url"));
            routes.Add("Environment", ConfigurationHelper.GetConfiguration("tNext.Microservices.Environment.Api.Url"));
            routes.Add("Health", ConfigurationHelper.GetConfiguration("tNext.Microservices.Health.Api.Url"));
            routes.Add("Bff-Mobile", ConfigurationHelper.GetConfiguration("tNext.Bff.Mobile.Api.Url"));
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {

                UserHelper.AddCustomerInformationToRequest(request);

                var queryParameters = request.RequestUri.Query;
                var microservice = request.GetRouteData().Values["microservice"].ToString();
                var urlParts = (request.GetRouteData().Values["urlParts"] ?? "").ToString();

                var microserviceUrl = routes[microservice];

                var newUrl = $"{microserviceUrl}/{urlParts}{queryParameters}";

                var uribuilder = new UriBuilder(newUrl);
                request.RequestUri = uribuilder.Uri;
                HttpClient client = new HttpClient();
                if (request.Method == HttpMethod.Get)
                {
                    request.Content = null;
                }
                request.Headers.Host = uribuilder.Host;

                var originalResponse = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                if (originalResponse.IsSuccessStatusCode)
                {
                    return originalResponse;
                }
                else
                {
                    tNextMicroserviceCallError tnextCallError = await Utils.GetTnextErrorFromHttpResponseMessage(originalResponse);
                    tnextCallError.InternalMessage = tnextCallError.InternalMessage + ", Request Headers: " + request.GetHeadersAsJsonString()+ ", Reason Phrase" + originalResponse.ReasonPhrase;
                    return tNextResponseCreator.NOK(originalResponse.StatusCode, tnextCallError.MaskForClientApplications());
                }

            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
                var error = new ApiGatewayError(code: HttpStatusCode.InternalServerError.ToString(), internalMessage: $"{(ex.InnerException ?? ex).ToString()} - StackTrace:{ex.StackTrace}", externalMessage: "Hata oluştu");
                return tNextResponseCreator.NOK(HttpStatusCode.InternalServerError, error.MaskForClientApplications());
            }

        }

    }
}