using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using tNext.Common.Model.Enums;

namespace tNext.Common.Core.Helpers
{
    /// <summary>
    /// Ip resolver class
    /// </summary>
    public static class IpHelper
    {
        /// <summary>
        /// Get client ip 
        /// </summary>
        /// <param name="request">Http request</param>
        /// <returns>Gets ip from header, if not exists get it from requester</returns>
        public static string GetApplicationIp()
        {
            HttpRequest request = null;

            if (request == null && HttpContext.Current != null)
                request = HttpContext.Current.Request;

            if (request == null)
                return string.Empty;

            return request.Headers.Get(Headers.ApplicationIp) ?? GetApplicationIpFromRequestHeader();
        }

        private static string GetApplicationIpFromRequestHeader()
        {
            try
            {
                var ip = HttpContext.Current.Request.UserHostAddress;
                if (HttpContext.Current.Request.Headers["X-Forwarded-For"] != null)
                {
                    ip = HttpContext.Current.Request.Headers["X-Forwarded-For"];
                }
                else if (HttpContext.Current.Request.Headers["REMOTE_ADDR"] != null)
                {
                    ip = HttpContext.Current.Request.Headers["REMOTE_ADDR"];
                }
                return ip;
            }
            catch (Exception ex)
            {
                //TODO: burayı loglamak lazım
            }
            return string.Empty;
        }
    }
}
