using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Web;
using tNext.Common.Core.Model;

namespace tNext.Common.Core.Helpers
{
    public class UriHelper
    {
        public static string GetUserFriendlyUri(string rawUri)
        {
            Regex _userFriendlyUrlMatcher = new Regex("[a-zA-Z0-9_]*");
            List<string> exceptForList = new List<string>() { "a+", "a++", "a+++", "a++++", "a+++++", "a++++++" };
            Regex rx1 = new Regex(@"[^a-z0-9\s_,-]*", RegexOptions.Singleline | RegexOptions.Compiled);
            Regex rx2 = new Regex(@"[\s-,_]+", RegexOptions.Singleline | RegexOptions.Compiled);
            Regex rx3 = new Regex(@"\s", RegexOptions.Singleline | RegexOptions.Compiled);
            Regex rx4 = new Regex(@"[^a-z0-9_,-]*", RegexOptions.Singleline | RegexOptions.Compiled);

            if (!String.IsNullOrWhiteSpace(rawUri))
            {
                const string underscore = "_";
                if (HttpContext.Current != null)
                    rawUri = HttpContext.Current.Server.HtmlDecode(rawUri);

                while (rawUri.StartsWith(underscore))
                {
                    rawUri = rawUri.Remove(0, 1);
                }

                while (rawUri.EndsWith(underscore))
                {
                    rawUri = rawUri.Substring(0, rawUri.Length - 1);
                }


                rawUri = rawUri.ToLowerInvariant()
                                    .Replace("ç", "c")
                                    .Replace("ş", "s")
                                    .Replace("ü", "u")
                                    .Replace("ğ", "g")
                                    .Replace("&", "ve")
                                    .Replace("ö", "o")
                                    .Replace("ı", "i")
                                    .Replace("İ", "i");

                int maxLength = 50;

                if (!exceptForList.Contains(rawUri))
                {
                    rawUri = rx1.Replace(rawUri, "");
                }

                rawUri = rx2.Replace(rawUri, " ").Trim();
                if (rawUri.Length > maxLength)
                    rawUri = rawUri.Substring(0, maxLength).Trim();
                rawUri = rx3.Replace(rawUri, "-");

                return rawUri;
            }

            return null;
        }
        public static PagingModel GetPagingModel(int defaultRowCount = int.MaxValue)
        {
            int pageNumber = 0;
            int.TryParse(HttpHelper.GetQueryParameter("pageNumber"), out pageNumber);

            int rowCount = int.MaxValue;
            int.TryParse(HttpHelper.GetQueryParameter("rowCount"), out rowCount);

            int startIndex = 0;
            int.TryParse(HttpHelper.GetQueryParameter("startIndex"), out startIndex);

            if (rowCount == 0)
            {
                rowCount = defaultRowCount;
            }

            return new PagingModel()
            {
                PageNumber = pageNumber,
                RowCount = rowCount,
                StartIndex = startIndex
            };
        }

    }
}
