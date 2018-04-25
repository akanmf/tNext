using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;

namespace tNext.Common.Core.Rest
{
    /// <summary>
    /// Provides functionality for REST based requests.
    /// </summary>
    public class RestCall
    {
        private readonly IRestClient restClient;
        private IRestRequest restRequest;
        private readonly string address;

        /// <summary>
        /// Initializes a Restcall instance with the specified Url
        /// </summary>
        /// <param name="baseUrl">REST Url</param>
        public RestCall(string baseUrl)
        {
            this.restClient = new RestClient(baseUrl);
            this.address = baseUrl;
        }

        /// <summary>
        /// POSTs the request
        /// </summary>
        /// <returns>itself</returns>
        public RestCall Post()
        {
            return Communicate(Method.POST);
        }
        /// <summary>
        /// PUTs the request
        /// </summary>
        /// <returns>itself</returns>
        public RestCall Put()
        {
            return Communicate(Method.PUT);
        }

        /// <summary>
        /// GETs the request
        /// </summary>
        /// <returns>itself</returns>
        public RestCall Get()
        {
            return Communicate(Method.GET);
        }

        /// <summary>
        /// DELETEs the request
        /// </summary>
        /// <returns>itself</returns>
        public RestCall Delete()
        {
            return Communicate(Method.DELETE);
        }

        /// <summary>
        /// Internal communicator that processes the request according to the required method
        /// </summary>
        /// <param name="method">Request method</param>
        /// <returns>itself</returns>
        private RestCall Communicate(RestSharp.Method method)
        {
            this.restRequest = new RestRequest(method)
            {
                RequestFormat = DataFormat.Json,
                JsonSerializer = new RestSharpJsonNetSerializer()
            };

            //TODO: buraya request debth eklenmeli. Aşağıdaki şekilde hata alıyor. Bu şekilde olmamalı
            //this.restRequest.AddHeader(Headers.RequestDepth, tNextExecutionContext.Current.RequestDebth.ToString());

            return this;
        }

        /// <summary>
        /// Add headers to new request
        /// </summary>
        /// <param name="headerDictionary">key - value pairs</param>
        /// <returns>itself</returns>
        public RestCall WithHeaders(IDictionary<string, string> headerDictionary)
        {
            foreach (var header in headerDictionary)
            {
                var parameter = this.restRequest.Parameters.FirstOrDefault(s => s.Name == header.Key);
                if (parameter != null)
                    parameter.Value = header.Value;
                else
                    this.restRequest.AddHeader(header.Key, header.Value);
            }

            return this;
        }

        /// <summary>
        /// Add header details from execution context
        /// </summary>        
        /// <returns>itself</returns>
        public RestCall WithContextHeaders()
        {
            if (tNextExecutionContext.Current != null)
            {
                this.restRequest.AddHeader(Common.Model.Enums.Headers.ApplicationIp, tNextExecutionContext.Current.ApplicationIp);
                this.restRequest.AddHeader(Common.Model.Enums.Headers.RequestId, tNextExecutionContext.Current.RequestId);
                this.restRequest.AddHeader(Common.Model.Enums.Headers.ClientName, tNextExecutionContext.Current.ApplicationName);
                this.restRequest.AddHeader(Common.Model.Enums.Headers.OperatorId, tNextExecutionContext.Current.OperatorId);
            }
            return this;
        }

        /// <summary>
        /// Body
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public RestCall IncludingBody(object data)
        {
            this.restRequest.AddBody(data);

            return this;
        }

        /// <summary>
        /// Parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RestCall IncludingParameter(string name, object data)
        {
            this.restRequest.AddParameter(name, data);

            return this;
        }

        /// <summary>
        /// Parameter
        /// </summary>
        /// <param name="parametersDictionary"></param>
        /// <returns></returns>
        public RestCall IncludingParameters(IDictionary<string, object> parametersDictionary)
        {
            foreach (var parameter in parametersDictionary)
                this.restRequest.AddParameter(parameter.Key, parameter.Value);

            return this;
        }

        /// <summary>
        /// Parameter
        /// </summary>
        /// <param name="name"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public RestCall IncludingQueryParameter(string name, string data)
        {
            this.restRequest.AddQueryParameter(name, data);

            return this;
        }

        /// <summary>
        /// Parameter
        /// </summary>
        /// <param name="queryParametersDictionary"></param>
        /// <returns></returns>
        public RestCall IncludingQueryParameters(IDictionary<string, string> queryParametersDictionary)
        {
            foreach (var queryParameter in queryParametersDictionary)
                this.restRequest.AddQueryParameter(queryParameter.Key, queryParameter.Value);

            return this;
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage Send()
        {
            try
            {
                tNextLogManager.Log(JsonConvert.SerializeObject(new { requestUrl = this.address, param = this.restRequest.Parameters }));
                var response = this.restClient.Execute(this.restRequest);
                tNextLogManager.Log(JsonConvert.SerializeObject(new { responseUrl = response.ResponseUri, param = response.Content }));

                var responseModel = JsonConvert.DeserializeObject<tNextResponse>(response.Content);

                if (response.StatusCode == HttpStatusCode.OK)
                    return new tNextResponseCreator()
                        .WithStatusCode(HttpStatusCode.OK)
                        .IncludingData(responseModel.Data)
                        .CreateResponse();

                var message = new HttpResponseMessage(response.StatusCode);
                message.Content = new StringContent(response.Content);

                message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                return message;
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <returns></returns>
        public tNextResponse SendAndGetResponse()
        {
            try
            {
                tNextLogManager.Log(JsonConvert.SerializeObject(new { requestUrl = this.address, param = this.restRequest.Parameters }));
                var response = this.restClient.Execute(this.restRequest);
                tNextLogManager.Log(JsonConvert.SerializeObject(new { responseUrl = response.ResponseUri, param = response.Content }));

                return JsonConvert.DeserializeObject<tNextResponse>(response.Content);
            }
            catch (Exception e)
            {
                tNextLogManager.LogError(e);
                throw;
            }
        }

        /// <summary>
        /// Send
        /// </summary>
        /// <typeparam name="TModel"></typeparam>
        /// <returns></returns>
        public TModel Send<TModel>() where TModel : class, new()
        {
            try
            {
                tNextLogManager.Log(JsonConvert.SerializeObject(new { requestUrl = this.address, param = this.restRequest.Parameters }));
                var response = this.restClient.Execute<TModel>(this.restRequest);
                tNextLogManager.Log(JsonConvert.SerializeObject(new { responseUrl = response.ResponseUri, param = response.Content }));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseModel = JsonConvert.DeserializeObject<tNextResponse>(response.Content);

                    return JsonConvert.DeserializeObject<TModel>(responseModel.Data.ToString());
                }

                return null;
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
                throw;
            }
        }

        /// <summary>
        /// SendAsync
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage SendAsync()
        {
            try
            {
                tNextLogManager.Log(JsonConvert.SerializeObject(new { requestUrl = this.address, param = this.restRequest.Parameters }));
                this.restClient.ExecuteAsync(this.restRequest, restResponse =>
                {
                    tNextLogManager.Log(JsonConvert.SerializeObject(new { responseUrl = restResponse.ResponseUri, param = restResponse.Content }));
                });

                return new tNextResponseCreator().CreateResponse();
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
                throw;
            }
        }
    }
}
