using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;

namespace tNext.Common.Core.Filters
{
    public class CacheAttribute : ActionFilterAttribute
    {
        public CacheAttribute(CacheType cacheType, bool clientCaching = true, uint cacheDuration = 15 * 60)
        {
            this.cacheType = cacheType;
            this.ClientCaching = clientCaching;
            this.CacheDuration = cacheDuration;
        }

        private CacheControlHeaderValue GetCacheHeader()
        {
            return new CacheControlHeaderValue
            {
                Public = true,
                NoCache = false,
                NoStore = false,
                MaxAge = TimeSpan.FromSeconds(CacheDuration)
            };
        }

        /// <summary>
        /// Duration in seconds.
        /// </summary>
        public uint CacheDuration { get; set; }

        /// <summary>
        /// Enable cliend side caching. 
        /// </summary>
        public bool ClientCaching { get; set; }

        CacheType cacheType;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string cacheKey = GetCacheKey(actionContext);

            var cachedData = (cacheType == CacheType.Local) ? CacheHelper.Local.Get<string>(cacheKey) : CacheHelper.Remote.Get<string>(cacheKey);

            //Lokalde development yaparken cache leme yapmasın
            if (cachedData == null)
            {
                base.OnActionExecuting(actionContext);
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse();
                actionContext.Response.Content = new StringContent(cachedData, Encoding.UTF8, "application/json");
                actionContext.Response.Headers.CacheControl = GetCacheHeader();
            }
        }

        private string GetCacheKey(HttpActionContext actionContext)
        {
            var cacheKey = $"{actionContext.ActionDescriptor.ControllerDescriptor.ControllerName}-{actionContext.ActionDescriptor.ActionName}-";
            if (actionContext.ActionArguments != null && actionContext.ActionArguments.Count > 0)
            {
                cacheKey += string.Join("-", actionContext.ActionArguments.Select(a => (a.Value ?? string.Empty).ToString()));
            }

            var pagingInfo = UriHelper.GetPagingModel();
            cacheKey += $"{pagingInfo.PageNumber}{pagingInfo.RowCount}{pagingInfo.StartIndex}";

            cacheKey = cacheKey.Replace(" ", "").ToLower();

            return cacheKey;
        }

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {

            if (actionExecutedContext.Exception != null)
            {
                return;
            }

            var cacheKey = GetCacheKey(actionExecutedContext.ActionContext);
            var content = actionExecutedContext.Response.Content.ReadAsStringAsync().Result;
            var tnextResponse = JsonConvert.DeserializeObject<tNextResponse>(content);
            if (actionExecutedContext.ActionContext.Response.StatusCode == System.Net.HttpStatusCode.OK && tnextResponse.IsSuccess
                && String.Compare(tNextMicroservice.Environment, Environments.Local, true) != 0 //lokalde cache leme yapmasın
                )
            {
                switch (cacheType)
                {
                    case CacheType.Local:
                        CacheHelper.Local.Set(cacheKey, content, CacheDuration);
                        break;
                    case CacheType.Remote:
                        CacheHelper.Remote.Set(cacheKey, content, CacheDuration);
                        break;
                    default:
                        CacheHelper.Local.Set(cacheKey, content, CacheDuration);
                        break;
                }


                if (ClientCaching)
                {
                    actionExecutedContext.Response.Headers.Add(Headers.ClientCaching, ClientCaching.ToString());

                    if (String.Compare(tNextMicroservice.Environment, Environments.Local, true) != 0)
                        actionExecutedContext.Response.Headers.CacheControl = GetCacheHeader();
                }

                actionExecutedContext.Response.Headers.Add(Headers.ServerSideCaching, "true");

                actionExecutedContext.Response.Headers.Add(Headers.CacheDuration, CacheDuration.ToString());

            }

            base.OnActionExecuted(actionExecutedContext);
        }
    }
}


