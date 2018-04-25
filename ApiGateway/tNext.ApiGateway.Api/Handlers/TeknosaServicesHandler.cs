using Newtonsoft.Json;
using System;
using System.Net;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using tNext.ApiGateway.Api.Model;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model.Errors;

namespace tNext.ApiGateway.Api.Handlers
{
    public class TeknosaServicesHandler : DelegatingHandler
    {


        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            RelayInfo relayInfo;
            try
            {
                relayInfo = new RelayInfo
                {
                    ServiceName = "TeknosaServices",
                    Url = (request.GetRouteData().Values["urlParts"] ?? "").ToString(),
                    UrlParameters = request.RequestUri.Query
                };

                relayInfo.RouteUrl = $"{ConfigurationHelper.GetConfiguration("Teknosa.MobileService.Url")}/TeknosaMobileServices.asmx/{relayInfo.Url}";

                var uribuilder = new UriBuilder(relayInfo.RouteUrl);
                request.RequestUri = uribuilder.Uri;
                request.Headers.Host = uribuilder.Host;


                using (var handler = new HttpClientHandler())
                {
                    using (HttpClient client = new HttpClient(handler))
                    {

                        var cookieList = request.Headers.GetCookies();
                        if (cookieList != null && cookieList.Count > 0 && cookieList[0].Cookies != null)
                        {
                            foreach (var cookie in cookieList[0].Cookies)
                            {
                                handler.CookieContainer.Add(uribuilder.Uri, new Cookie(cookie.Name, cookie.Value));
                            }
                        }


                        if (request.Method == HttpMethod.Get)
                        {
                            request.Content = null;
                        }

                        var originalResponse = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead);

                        HttpResponseMessage response = await GettNextResponse(originalResponse, relayInfo);
                        return response;
                    }
                }
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
                var error = new ApiGatewayError(code: HttpStatusCode.InternalServerError.ToString(), internalMessage: (ex.InnerException ?? ex).ToString() + " (Probably Timeout Issue)", externalMessage: "Hata oluştu");
                return tNextResponseCreator.NOK(HttpStatusCode.InternalServerError, error.MaskForClientApplications());
            }
        }

        private async Task<HttpResponseMessage> GettNextResponse(HttpResponseMessage originalResponse, RelayInfo relayInfo)
        {

            var _tNextResponse = new tNextResponse();



            if (originalResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var str = await originalResponse.Content.ReadAsStringAsync();
                var ins = originalResponse.Content.ReadAsAsync<RootObject>();

                RootObject orgResponse = ins.Result;

                if (orgResponse.d.Code != 0 && orgResponse.d.Code != 200)
                {
                    _tNextResponse.Error = new TeknosaServicesCallError(
                        code: orgResponse.d.Code.ToString(),
                        internalMessage: $"{relayInfo.RouteUrl} çağrılırken hata oluştu. Code:{orgResponse.d.Code}, Message:{orgResponse.d.InternalMessage}",
                        externalMessage: orgResponse.d.Message
                    );
                }

                _tNextResponse.IsSuccess = (_tNextResponse.Error == null);

                if (_tNextResponse.IsSuccess)
                {
                    str = str.Replace("\"d\":", "");
                    str = str.Substring(1, str.Length - 2);
                    str = str.Replace($"\"__type\":\"{orgResponse.d.__type}\",", "");
                    str = str.Replace($"\"Code\":{orgResponse.d.Code},", "");
                    _tNextResponse.Data = JsonConvert.DeserializeObject(str);
                }

            }
            else
            {
                _tNextResponse.IsSuccess = false;

                _tNextResponse.Error = new TeknosaServicesCallError(
                    externalMessage: "Hata oluştu",
                    internalMessage: $"{relayInfo.RouteUrl} çağrılırken hata oluştu. {originalResponse.ReasonPhrase }: {originalResponse.GetBodyAsJsonString()}"
                );
            }

            _tNextResponse.Error.MaskForClientApplications();

            string responseString = JsonConvert.SerializeObject(_tNextResponse);

            originalResponse.Content = new StringContent(responseString, Encoding.UTF8, "application/json");

            return originalResponse;
        }


        public class D
        {
            public string __type { get; set; }
            public int Code { get; set; }
            public string Message { get; set; }
            public string Token { get; set; }
            public string InternalMessage { get; set; }
        }

        public class RootObject
        {
            public D d { get; set; }
        }
    }
}