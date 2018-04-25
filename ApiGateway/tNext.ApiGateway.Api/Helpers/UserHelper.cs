using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Helpers;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;

namespace tNext.ApiGateway.Api.Helpers
{
    public static class UserHelper
    {
        public static void AddCustomerInformationToRequest(HttpRequestMessage request)
        {
            var deviceId = request.Headers.GetHeader("DeviceId", "");
            var _token = request.Headers.GetHeader("Token", "");

            if (!string.IsNullOrEmpty(_token) && !string.IsNullOrEmpty(deviceId))
            {
                Guid token = new Guid(_token);
                var key = $"userToken-{deviceId}-{token}";
                var userCache = CacheHelper.Local.GetOrSet(key, 30 * 60, () =>
                {
                    var userObject = GetCurrentUserObject(deviceId, token);
                    string userStr = null;
                    if (userObject != null)
                    {
                        userStr = JsonConvert.SerializeObject(userObject);
                        userStr = StringHelper.CleanString(userStr);
                    }
                    return userStr;
                });

                if (!string.IsNullOrEmpty(userCache))
                {
                    request.Headers.Add(Headers.Customer, userCache);
                }
            }
        }

        public static UserObject GetCurrentUserObject(string deviceId, Guid token)
        {

            var customerUrl = $"{ConfigurationHelper.GetConfiguration("tNext.Microservices.Customer.Api.Url")}/Info/{deviceId}/{token}";
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, customerUrl);

            HttpClient client = new HttpClient();
            if (request.Method == HttpMethod.Get)
            {
                request.Content = null;
            }

            var task = client.SendAsync(request, HttpCompletionOption.ResponseContentRead);
            task.Wait();
            var task2 = task.Result.Content.ReadAsStringAsync();
            task2.Wait();

            var userStr = task2.Result;

            var dynUserTnextResponse = Json.Decode<tNextResponse>(userStr);

            if (dynUserTnextResponse.IsSuccess && dynUserTnextResponse.Data != null)
            {
                //TODO: burayı düzelt. Tek tek almak yerine topluca alan bişey yapılmalı
                var Data = (Dictionary<string, object>)dynUserTnextResponse.Data;
                var response = new UserObject
                {
                    g_user_id = Data["g_user_id"].ToString(),
                    u_ceptel_extension = Data["u_ceptel_extension"].ToString(),
                    u_ceptel_number = Data["u_ceptel_number"].ToString(),
                    u_email_address = Data["u_email_address"].ToString(),
                    u_first_name = Data["u_first_name"].ToString(),
                    u_last_name = Data["u_last_name"].ToString()
                };
                return response;
            }
            else
            {
                return null;
            }
        }


    }
}