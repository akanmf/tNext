using System;
using System.Linq;
using System.Net.Http;
using tNext.ApiGateway.Api.Helpers;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;

namespace tNext.ApiGateway.Api
{
    public static class HttpResponseMessageExtensions
    {
        public static void UpdateRequestLogEntity(this HttpResponseMessage response, RequestLog requestLogEntity)
        {
            try
            {
                requestLogEntity.ResponseTime = DateTime.Now;
                requestLogEntity.ResponseHeaders = response.GetHeadersAsJsonString();

                if (isAllowedResponseBodyDBLogging())
                    requestLogEntity.ResponseBody = response.GetBodyAsJsonString();
                else
                    requestLogEntity.ResponseBody = string.Empty;

                requestLogEntity.Description = requestLogEntity.Description ?? "" + response.ReasonPhrase;
                requestLogEntity.ResponseStatus = response.StatusCode.ToString();
            }
            catch (Exception ex)
            {
                requestLogEntity.Description = requestLogEntity.Description ?? "" + ex.ToString();
            }
        }

        public static string GetBodyAsJsonString(this HttpResponseMessage response)
        {
            try
            {
                var task = response.Content.ReadAsStringAsync();
                task.Wait();

                return task.Result;
            }
            catch (Exception ex)
            {
                return $"Response body okunurken hata alındı. Reason: {response.StatusCode} / {response.ReasonPhrase}. Exception: {ex.ToString()}";
            }
        }

        public static string GetHeadersAsJsonString(this HttpResponseMessage response)
        {
            var headers = response.Headers.Select(h => $"{h.Key}:{String.Join(" ", h.Value.ToList()[0])}");
            var result = string.Join("\n", headers);
            return result;
        }
        private static bool isAllowedResponseBodyDBLogging()
        {
            var forbiddenMethods = ConfigurationHelper.GetConfiguration("ForbiddenMethodsForResponseBodyDBLogging").Split(',');
            var requestedMethod = RequestHelper.GetRequestedMethodName();
            if (forbiddenMethods.Any(x => x.ToUpper() == requestedMethod.ToUpper()))
                return false;
            return true;
        }
    }
}