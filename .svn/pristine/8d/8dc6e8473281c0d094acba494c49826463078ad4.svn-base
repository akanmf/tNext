using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Http;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model.Errors;

namespace tNext.ApiGateway.Api
{
    public static class HttpRequestMessageExtensions
    {
        public static RequestLog GetRequestLogEntity(this HttpRequestMessage request)
        {
            var requestLog = new RequestLog();
            try
            {

                requestLog.ClientIp = IpHelper.GetApplicationIp();
                requestLog.Host = request.RequestUri.Host;
                requestLog.RequestBody = request.GetBodyAsJsonString();
                requestLog.RequestTime = DateTime.Now;
                requestLog.RequestHeaders = request.GetHeadersAsJsonString();
                requestLog.Url = request.RequestUri.PathAndQuery;
            }
            catch (Exception ex)
            {
                requestLog.Description = "Request parse edilirken hata:" + ex.ToString();
                return requestLog;
            }

            return requestLog;
        }

        public static string GetBodyAsJsonString(this HttpRequestMessage request)
        {
            try
            {
                if (request.Method == HttpMethod.Get)
                {
                    return string.Empty;
                }
                if (request.GetRouteData().Values["urlParts"].ToString().Contains("UpdateCreditCardInformation"))
                {
                    return "*";
                }
                var task = request.Content.ReadAsStringAsync();
                task.Wait();

                return task.Result;
            }
            catch (Exception ex)
            {
                var response = new tNextResponse();
                response.IsSuccess = false;
                response.Error = new ApiGatewayError();
                response.Error.InternalMessage = "Body okunurken hata alındı: " + ex.ToString();

                var resultStr = JsonConvert.SerializeObject(response, DefaultSerializerSettings.Settings);
                return resultStr;
            }
        }

        public static string GetHeadersAsJsonString(this HttpRequestMessage request)
        {
            var headers = request.Headers.Select(h => $"{h.Key}:{String.Join(" ", h.Value.ToList()[0])}");
            var result = string.Join("\n", headers);
            return result;
        }

    }
}