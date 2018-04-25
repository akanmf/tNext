using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Core.Helpers;
using System.Web.Http.Controllers;
using System.Web;
using tNext.Common.Model.Enums;
using System.Runtime.Remoting.Messaging;
using tNext.Common.Core.Model;
using Newtonsoft.Json;

namespace tNext
{
    /// <summary>
    /// Execution context
    /// </summary>    
    [Serializable]
    public class tNextExecutionContext
    {
        private bool m_IsReadOnly = false;
        private string m_clientName;


        UserObject _customer;
        public UserObject Customer
        {
            get
            {
                if (_customer == null)
                {
                    var customerHeader = HttpContext.Current?.Request.Headers.GetHeader(Headers.Customer, "");
                    if (!string.IsNullOrEmpty(customerHeader))
                    {
                        _customer = JsonConvert.DeserializeObject<UserObject>(customerHeader);
                    }
                }
                return _customer;
            }

            set
            {
                // testlerde kullanılıyor
                _customer = value;
            }
        }

        private static readonly object LockObject = new object();


        private static tNextExecutionContext sm_current
        {
            get
            {
                return CallContext.LogicalGetData("ExecutionContext") as tNextExecutionContext;
            }
            set
            {
                CallContext.LogicalSetData("ExecutionContext", value);
            }
        }

        /// <summary>
        /// Current request's execution context
        /// </summary>
        public static tNextExecutionContext Current
        {
            get
            {
                if (sm_current == null)
                {
                    lock (LockObject)
                    {
                        if (sm_current == null)
                        {
                            sm_current = new tNextExecutionContext();
                        }
                    }
                }
                return sm_current;
            }
            internal set
            {
                lock (LockObject)
                {
                    sm_current = value;
                }
            }
        }


        /// <summary>
        /// constructor with a httprequest
        /// </summary>
        /// <param name="request"></param>
        public tNextExecutionContext()
        {
            this.IsUnitTest = false;
            m_IsReadOnly = true;

            var request = HttpContext.Current?.Request;
            if (request == null)
                return;

            UserAgent = request.UserAgent;
            RequestQuery = request.QueryString.ToString();
            RequestUri = request.Url.ToString();
            RequestHost = request.UserHostName;

            var headers = request.Headers;

            ApplicationIp = headers.GetHeader(Headers.ApplicationIp, IpHelper.GetApplicationIp());
            RequestId = headers.GetHeader(Headers.RequestId, string.Empty);
            m_clientName = headers.GetHeader(Headers.ClientName, string.Empty);

            //TODO: trace'i her önüne gelen tru yapamamalı. Bunun için bir yetki kontrolü yapılmalı
            TraceEnabled = headers.GetHeader(Headers.TraceEnabled, "false") == "true";

            OperatorId = headers.GetHeader(OperatorId, string.Empty);

        }

        private void IsReadOnly()
        {
            if (m_IsReadOnly && !this.IsUnitTest)
                throw new ArgumentException("Can not access this field in run-time");
        }


        public HttpContext HttpContext { get { return HttpContext.Current; } }

        public bool TraceEnabled { get; internal set; }

        /// <summary>
        /// requestid comes from apigateway
        /// </summary>
        public string RequestId { get; set; }
        /// <summary>
        /// Client name, if context isunittest then writable, otherwise will generate exception
        /// </summary>
        public string ApplicationName
        {
            get { return m_clientName; }
            set
            {
                IsReadOnly();
                m_clientName = value;
            }
        }
        /// <summary>
        /// if exists operator id
        /// </summary>
        public string OperatorId { get; set; }
        /// <summary>
        /// client id
        /// </summary>
        public string ApplicationIp { get; internal set; }

        /// <summary>
        /// request host ip address
        /// </summary>
        public string RequestHost { get; internal set; }
        /// <summary>
        /// requested url
        /// </summary>
        public string RequestUri { get; internal set; }
        /// <summary>
        /// request query
        /// </summary>
        public string RequestQuery { get; internal set; }
        /// <summary>
        /// User browser/agent
        /// </summary>
        public string UserAgent { get; internal set; }
        /// <summary>
        /// Check for if running context is unit test (default false)
        /// </summary>
        public bool IsUnitTest { get; internal set; }

        public int RequestDebth
        {
            get
            {
                int requestDebth = 0;
                int.TryParse(HttpContext.Current.Request.Headers.GetHeader(Headers.RequestDepth, ""), out requestDebth);
                return requestDebth;
            }
        }
        public PlatformType RequestPlatform { get { return PlatformHelper.DetectPlatform(); } }
        /// <summary>
        /// all keys prior to push on server
        /// </summary>
        public Dictionary<string, string> AllKeys { get; set; }
    }
}
