using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using tNext.ApiGateway.Api.Helpers;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;
using tNext.Common.Model.Errors;

namespace tNext.ApiGateway.Api.Handlers
{
    public class CommonMessageHandler : DelegatingHandler
    {
        Services.RequestLogService requestLogService = new Services.RequestLogService();

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (String.Compare(ConfigurationManager.AppSettings["UnderConstruction"] ?? "", "true", true) == 0)
            {
                var error = new UnderConstructionError();
                return Task.Run(() => tNextResponseCreator.NOK(HttpStatusCode.OK, error.MaskForClientApplications()));
            }
            var mobileTestDevices = string.Empty; ;

            if (ConfigurationHelper.TryGetConfiguration("MobileTestDevices", out mobileTestDevices))
            {
                var mobileTestDevicesArray = mobileTestDevices.Split(',');
                List<string> mobileTestDevicesList = new List<string>(mobileTestDevicesArray);
                if (IsMobileTestDevice(request, mobileTestDevicesList))
                {
                    var requestForTestapi = ChangeRequestUriForTestapi(request);
                    return requestForTestapi;
                }
            }

            var apiGatewayAllowedHeaders = string.Empty;
            if (ConfigurationHelper.TryGetConfiguration("ApiGatewayAllowedHeaders", out apiGatewayAllowedHeaders))
            {
                var apiGatewayAllowedHeadersArray = apiGatewayAllowedHeaders.ToLower().Split(',');
                List<string> apiGatewayAllowedHeadersList = new List<string>(apiGatewayAllowedHeadersArray);

                if (!apiGatewayAllowedHeadersList.Contains("*"))
                {
                    if (!IsAuthorizedHeader(request, apiGatewayAllowedHeadersList))
                        return Task.Run(() => request.CreateErrorResponse(HttpStatusCode.BadRequest, "Unauthorized Headers"));
                }
            }
            var isDatabaseRequestLoggingEnabled = string.Empty;
            Common.Core.Model.RequestLog requestLog = null;
            if (ConfigurationHelper.TryGetConfiguration("isDatabaseRequestLoggingEnabled", out isDatabaseRequestLoggingEnabled) && string.Equals(isDatabaseRequestLoggingEnabled, "true", StringComparison.CurrentCultureIgnoreCase))
            {
                requestLog = request.GetRequestLogEntity();
            }
            //TODO:this handler should be divide two or more piece. LoggingHandler,AuthenticationHandler etc.            

            var clientName = request.Headers.GetHeader(Headers.ClientName, "");
            var password = request.Headers.GetHeader(Headers.ClientPassword, "");
            var requestId = request.Headers.GetHeader(Headers.RequestId, "");


            if (string.IsNullOrEmpty(requestId))
            {
                var newRequestId = $"{tNextExecutionContext.Current.ApplicationIp}_{clientName}_{DateTime.Now.Ticks}_{Guid.NewGuid()}";
                request.Headers.Add(Headers.RequestId, newRequestId);
            }

            bool tokenActive = TokenLastTransactionTimeCheck(request);
            if (!tokenActive)
            {
                request.Headers.Remove(Headers.Token);
                HttpContext.Current.Response.Headers.Add(Headers.RefreshToken, "true");
            }

            int requestDebth = 1;
            int.TryParse(request.Headers.GetHeader(Headers.RequestDepth, ""), out requestDebth);
            request.Headers.Add(Headers.RequestDepth, requestDebth.ToString());

            HttpContext.Current.Response.Headers.Add(Headers.RequestId, request.Headers.GetHeader(Headers.RequestId, ""));

            HttpContext.Current.Response.Headers.Add(Headers.ApplicationIp, IpHelper.GetApplicationIp());

            HttpContext.Current.Response.Headers.Add(Headers.XForwardedTo, IpHelper.GetApplicationIp());

            var result = base.SendAsync(request, cancellationToken);
            result.ContinueWith(task =>
            {
                //TODO daha güzel bi if koy
                if (requestLog != null && !requestLog.Url.ToLower().Contains("swagger") && isAllowedMethodForDBLogging(request))
                {
                    task.Result.UpdateRequestLogEntity(requestLog);
                    requestLog.RequestId = request.Headers.GetHeader(Headers.RequestId, "--");
                    requestLogService.InsertReqeustLogAsync(requestLog);
                }

            });
            return result;
        }

        private bool TokenLastTransactionTimeCheck(HttpRequestMessage request)
        {
            //20 dakika süre ile işlem yapmamışsa hata vericez.
            var _token = request.Headers.GetHeader(Headers.Token, "");
            if (!string.IsNullOrEmpty(_token))
            {
                Guid token = new Guid(_token);
                string cacheKey = $"tokentimeoutcheck-{token}";

                var tokenLastTransactionTimeoutInMinutes = ConfigurationHelper.GetConfiguration("UserTokenLastTransactionTimeoutInMinutes");

                var tokenRenewTimeout = new TimeSpan(0, int.Parse(tokenLastTransactionTimeoutInMinutes), 0);

                var lastRequestTime = CacheHelper.Remote.Get<DateTime>(cacheKey);

                if (lastRequestTime > DateTime.MinValue && (DateTime.Now - lastRequestTime) > tokenRenewTimeout)
                {
                    return false;
                }
                CacheHelper.Remote.Set(cacheKey, DateTime.Now, 24 * 60 * 60);
            }
            return true;
        }

        private bool IsIntranetRequest()
        {
            //TODO: requestin intranettenmi yoksa dış dünyadanmı geldiği kontrol edilmeli
            return true;
        }

        private bool IsAuthorizedHeader(HttpRequestMessage request, List<string> _apiGatewayAllowedHeadersList)
        {
            if (request.Headers != null)
            {
                foreach (var header in request.Headers)
                {
                    if (!_apiGatewayAllowedHeadersList.Contains(header.Key.ToLower()))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        private bool IsAllowedClientAndPassword(string clientName, string password)
        {
            var allowedClientsAndPasswords = ConfigurationHelper.GetConfiguration("AllowedClientsAndPasswords");
            if (string.IsNullOrEmpty(allowedClientsAndPasswords))
                return false;

            foreach (var allowed in allowedClientsAndPasswords.Split(','))
            {
                var allowedClienName = allowed.Split(':')[0];
                var allowedPassword = allowed.Split(':')[1];
                if (string.Equals(allowedClienName, clientName) && string.Equals(allowedPassword, password))
                {
                    return true;
                }
            }
            return false;
        }
        private bool IsMobileTestDevice(HttpRequestMessage request, List<string> _mobileTestDevices)
        {
            if (_mobileTestDevices.Any(x => string.Compare(x, request.Headers.GetHeader(Headers.DeviceId, ""), true) == 0))
                return true;
            return false;

        }
        private Task<HttpResponseMessage> ChangeRequestUriForTestapi(HttpRequestMessage request)
        {
            var newUrl = $"{ConfigurationHelper.GetConfiguration("TestUrlToRoute")}{request.RequestUri.PathAndQuery}";
            var newUri = new UriBuilder(newUrl);
            request.RequestUri = newUri.Uri;
            request.Headers.Host = newUri.Host;

            using (var handler = new HttpClientHandler())
            {
                using (HttpClient client = new HttpClient(handler))
                {
                    var cookieList = request.Headers.GetCookies();
                    if (cookieList != null && cookieList.Count > 0 && cookieList[0].Cookies != null)
                    {
                        foreach (var cookie in cookieList[0].Cookies)
                        {
                            handler.CookieContainer.Add(newUri.Uri, new Cookie(cookie.Name, cookie.Value));
                        }
                    }
                    if (request.Method == HttpMethod.Get)
                    {
                        request.Content = null;
                    }
                    var op = client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
                    op.Wait();
                    return op;
                }
            }
        }

        private bool isAllowedMethodForDBLogging(HttpRequestMessage request)
        {
            var ForbiddenMethodsForDBLogging = String.Empty;

            if (ConfigurationHelper.TryGetConfiguration("ForbiddenMethodsForDBLogging", out ForbiddenMethodsForDBLogging))
            {
                List<string> ForbiddenMethodsForDBLoggingList = new List<string>(ForbiddenMethodsForDBLogging.Split(','));
                var requestedMethod = RequestHelper.GetRequestedMethodName();

                foreach (var item in ForbiddenMethodsForDBLoggingList)
                {
                    if (requestedMethod.ToUpper() == item.ToUpper())
                        return false;
                }
                return true;
            }

            return true;

        }
    }
}