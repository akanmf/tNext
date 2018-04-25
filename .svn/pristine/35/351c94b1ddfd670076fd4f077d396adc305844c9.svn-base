using System;
using System.Net.Http;
using System.Web.Http;
using tNext.Common.Core.Filters;
using tNext.Common.Core.Model;
using tNext.Common.Model.Errors;
using tNext.Microservices.Customer.Api.Services;

namespace tNext.Microservices.Customer.Api.Controllers
{
    public class CustomerController : ApiController
    {
        CustomerService customerService = new CustomerService();

        [Route("Info/{deviceId}/{token}")]
        [HttpGet]
        public HttpResponseMessage GetByDeviceIdAndToken(string deviceId, string token)
        {
            var customer = customerService.GetUserByDeviceIDAndToken(deviceId, token);
            if (customer == null)
            {
                return tNextResponseCreator.NOK(new CustomerNotFoundError("There is no customer that using the given token/device"));
            }
            return tNextResponseCreator.OK(customer);
        }

        [Route("InsertDeviceInformations/{operatingSystem}/{operatingSystemVersion}/{deviceManufacturer}/{deviceModel}/{pushNotificationToken}")]
        [HttpGet]
        [RequestFilter(customerRequired: true)]
        public HttpResponseMessage InsertDeviceInformations(string operatingSystem, string operatingSystemVersion, string deviceManufacturer, string deviceModel, string pushNotificationToken)
        {
            String userID = tNextExecutionContext.Current.Customer.g_user_id;
            var PlatformType = tNextExecutionContext.Current.RequestPlatform.ToString();

            var response = customerService.InsertDeviceInformations(userID, PlatformType, operatingSystem, operatingSystemVersion, deviceManufacturer, deviceModel, pushNotificationToken);
            return tNextResponseCreator.OK(response);
        }

        [Route("Firebase")]
        [HttpGet]
        [RequestFilter(customerRequired: true)]
        public HttpResponseMessage GetCustomerFirebaseInfo()
        {
            string userID = tNextExecutionContext.Current.Customer.g_user_id;
            var response = customerService.GetFirebaseInfo(userID);

            return tNextResponseCreator.OK(response);
        }
    }
}