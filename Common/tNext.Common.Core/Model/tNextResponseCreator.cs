using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Core.Model;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Core.Model
{
    /// <summary>
    /// General response data regarding REST.
    /// </summary>
    public class tNextResponseCreator
    {
        private readonly HttpRequestMessage request;
        private readonly HttpResponseMessage response;
        private readonly tNextResponse responseModel;



        /// <summary>
        /// Default constructor
        /// </summary>
        public tNextResponseCreator()
        {
            this.request = new HttpRequestMessage();
            this.response = this.request.CreateResponse();

            this.responseModel = new tNextResponse();
        }

        #region Static Utility Functions

        /// <summary>
        /// Shorthand successful rest response generator
        /// </summary>
        /// <returns></returns>
        public static HttpResponseMessage OK()
        {
            return OK(null);
        }

        /// <summary>
        /// Shorthand successful rest response generator
        /// </summary>
        /// <param name="data">Nullable field to return to client</param>
        /// <returns></returns>
        public static HttpResponseMessage OK(object data)
        {
            return Generate(HttpStatusCode.OK, data);
        }

        public static HttpResponseMessage NOK(tNextErrorBase error)
        {
            return Generate(statusCode: HttpStatusCode.OK, error: error);
        }

        public static HttpResponseMessage NOK(HttpStatusCode statusCode, tNextErrorBase error)
        {
            return Generate(statusCode: statusCode, data: null, error: error);
        }

        public static HttpResponseMessage Generate(HttpStatusCode statusCode, object data = null, tNextErrorBase error = null)
        {
            var res = new tNextResponseCreator().WithStatusCode(statusCode).IncludingData(data).IncludingError(error);
            return res.CreateResponse();
        }

        #endregion

        /// <summary>
        /// Add header
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public tNextResponseCreator AddHeader(string name, string value)
        {
            this.response.Headers.Add(name, value);
            return this;
        }

        /// <summary>
        /// Status Code
        /// </summary>
        /// <param name="statusCode"></param>
        /// <returns></returns>
        public tNextResponseCreator WithStatusCode(HttpStatusCode statusCode)
        {
            this.response.StatusCode = statusCode;

            return this;
        }

        /// <summary>
        /// Include data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public tNextResponseCreator IncludingData(object data)
        {
            this.responseModel.Data = data;
            return this;
        }



        /// <summary>
        /// Include error
        /// </summary>
        /// <param name="exception"></param>
        /// <returns></returns>
        public tNextResponseCreator IncludingError(tNextErrorBase error)
        {
            this.responseModel.Error = error;
            return this;
        }

        /// <summary>
        /// Create response
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage CreateResponse()
        {
            string data = "null";
            if (this.responseModel.Data != null)
            {
                data = JsonConvert.SerializeObject(this.responseModel.Data, DefaultSerializerSettings.Settings);

                //if (!data.IsNull())
                //{
                //    if (!data.StartsWith("["))
                //    {
                //        data = "[" + data + "]";
                //    }
                //}
            }

            string error = "null";

            if (this.responseModel.Error != null)
                error = JsonConvert.SerializeObject(this.responseModel.Error, DefaultSerializerSettings.Settings);

            string responseString = string.Format("{{ \"data\": {0}, \"error\": {1}, \"isSuccess\": {2} }}", data, error, (error.ToLower() == "null").ToString().ToLower());

            this.response.Content = new StringContent(responseString, Encoding.UTF8, "application/json");

            return this.response;
        }
    }
}
