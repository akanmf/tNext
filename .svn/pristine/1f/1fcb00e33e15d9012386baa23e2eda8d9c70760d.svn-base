using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Model.Errors
{
    public class tNextMicroserviceCallError : tNextErrorBase
    {
        public tNextMicroserviceCallError()
            : base("TNEXT_MICROSERVICE_ERROR", "Teknosa servisi çağrılırken hata meydana geldi", "Servis Hatası")
        {
        }


        public tNextMicroserviceCallError(string code = null, string internalMessage = null, string externalMessage = null)
            : base($"TNEXT_MICROSERVICE_ERROR_{code}", internalMessage, externalMessage)
        {
        }
    }
}
