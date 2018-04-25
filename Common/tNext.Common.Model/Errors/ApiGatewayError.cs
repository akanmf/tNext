using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Model.Errors
{
    public class ApiGatewayError : tNextErrorBase
    {
        public ApiGatewayError()
            : base("API_GATEWAY_ERROR", "Apigateway'de hata meydana geldi", "Apigateway Hatası")
        {
        }


        public ApiGatewayError(string code = null, string internalMessage = null, string externalMessage = null)
            : base($"API_GATEWAY_ERROR_{code}", internalMessage, externalMessage)
        {
        }
    }
}
