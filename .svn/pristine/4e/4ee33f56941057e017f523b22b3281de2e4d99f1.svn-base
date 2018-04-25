using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Routing;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;

namespace tNext.Common.Core.Handlers
{
    public class LoggingMessageHandler : DelegatingHandler
    {
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            int requestDebth = 0;
            int.TryParse(HttpContext.Current.Request.Headers.GetHeader(Headers.RequestDepth, ""), out requestDebth);
            if (requestDebth != 0)
            {
                HttpContext.Current.Request.Headers.Remove(Headers.RequestDepth);
                requestDebth++;
            }
            HttpContext.Current.Request.Headers.Add(Headers.RequestDepth, requestDebth.ToString());

            var apiLogEntry = CreateApiLogEntryWithRequestData(request);
            if (request.Content != null)
            {
                await request.Content.ReadAsStringAsync()
                       .ContinueWith(task =>
                     {
                         apiLogEntry.RequestContentBody = task.Result;
                     }, cancellationToken);
            }



            return await base.SendAsync(request, cancellationToken)
                .ContinueWith(task =>
                {

                    apiLogEntry.ResponseTimestamp = DateTime.Now;

                    if (task.Status != TaskStatus.RanToCompletion)
                    {
                        apiLogEntry.ResponseStatusCode = (int)HttpStatusCode.InternalServerError;
                        apiLogEntry.RequestContentBody = (task.Exception.InnerException ?? task.Exception).ToString();
                        apiLogEntry.ResponseStatusCode = (int)HttpStatusCode.InternalServerError;
                        tNextLogManager.Log(JsonConvert.SerializeObject(apiLogEntry));
                        var errorResponse = request.CreateErrorResponse(HttpStatusCode.InternalServerError, task.Exception);
                        return errorResponse;
                    }

                    var response = task.Result;

                    apiLogEntry.ResponseStatusCode = (int)response.StatusCode;




                    if (response.Content != null)
                    {
                        apiLogEntry.ResponseContentBody = response.Content.ReadAsStringAsync().Result;
                        apiLogEntry.ResponseContentType = response.Content.Headers.ContentType.MediaType;
                        apiLogEntry.ResponseHeaders = SerializeHeaders(response.Content.Headers);
                    }

                    tNextLogManager.Log(JsonConvert.SerializeObject(apiLogEntry));

                    return response;
                }, cancellationToken);
        }


        private ApiCallLogEntry CreateApiLogEntryWithRequestData(HttpRequestMessage request)
        {
            var context = ((HttpContextBase)request.Properties["MS_HttpContext"]);
            var routeData = request.GetRouteData();


            return new ApiCallLogEntry
            {
                Application = tNextExecutionContext.Current.ApplicationName,
                RequestId = tNextExecutionContext.Current.RequestId,
                User = context.User.Identity.Name,
                Machine = Environment.MachineName,
                RequestContentType = context.Request.ContentType,
                RequestRouteTemplate = routeData.Route.RouteTemplate,
                RequestRouteData = SerializeRouteData(routeData),
                RequestIpAddress =IpHelper.GetApplicationIp(),
                RequestMethod = request.Method.Method,
                RequestHeaders = SerializeHeaders(request.Headers),
                RequestTimestamp = DateTime.Now,
                RequestUri = request.RequestUri.ToString()
            };
        }


        private string SerializeRouteData(IHttpRouteData routeData)
        {
            // parametresiz servislerde route data olmadığından hata alıyor
            if (routeData.Values.Count <= 1)
            {
                return string.Empty;
            }
            return JsonConvert.SerializeObject(routeData, Formatting.Indented);
        }


        private string SerializeHeaders(HttpHeaders headers)
        {
            var dict = new Dictionary<string, string>();


            foreach (var item in headers.ToList())
            {
                if (item.Value != null)
                {
                    var header = String.Empty;
                    foreach (var value in item.Value)
                    {
                        header += value + " ";
                    }


                    // Trim the trailing space and add item to the dictionary 
                    header = header.TrimEnd(" ".ToCharArray());
                    dict.Add(item.Key, header);
                }
            }


            return JsonConvert.SerializeObject(dict, Formatting.Indented);
        }
    }

}
