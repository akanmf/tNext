using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Net.Http.Headers;
using System.Collections.Specialized;
using Newtonsoft.Json;

namespace tNext
{
    public static class HttpExtensions
    {
        public static HttpRequestMessage Clone(this HttpRequestMessage request, string newUrl)
        {

            var newUri = new Uri(newUrl);

            HttpRequestMessage clone = new HttpRequestMessage(request.Method, newUri);

            if (request.Method != HttpMethod.Get)
            {
                clone.Content = request.Content;
            }

            clone.Version = request.Version;

            foreach (var header in request.Headers)
            {
                if (new string[] { "Cookie", "clientName", "password" }.Contains(header.Key))
                {
                    clone.Headers.TryAddWithoutValidation(header.Key, header.Value);
                }
            }

            clone.Headers.Host = new Uri(newUrl).Authority;
            return clone;
        }



        /// <summary>
        /// Get header
        /// </summary>
        /// <param name="arr">arr</param>
        /// <param name="key">key</param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static string GetHeader(this HttpRequestHeaders arr, string key, string defaultValue)
        {
            IEnumerable<string> values = null;
            if (!arr.TryGetValues(key, out values))
                return defaultValue;

            return values.Any() ? (values.First() ?? defaultValue) : defaultValue;
        }


        /// <summary>
        /// Get header
        /// </summary>
        /// <param name="arr">arr</param>
        /// <param name="key">key</param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static string GetHeader(this NameValueCollection arr, string key, string defaultValue)
        {
            string newKey = arr.AllKeys.FirstOrDefault(s => s.Equals(key, StringComparison.InvariantCultureIgnoreCase));
            if (!string.IsNullOrEmpty(newKey))
                key = newKey.ToLowerInvariant();
            return arr.Get(key) ?? defaultValue;
        }

        public static string ToLogString(this HttpRequestMessage request)
        {
            //TODO: Dont forget to rewrite request.tologstr method
            var logStr = JsonConvert.SerializeObject(new { url = request.RequestUri.AbsolutePath, headers = request.Headers, });
            return logStr;
        }
    }
}
