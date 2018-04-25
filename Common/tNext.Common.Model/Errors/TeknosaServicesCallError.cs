using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Model.Errors
{
    public class TeknosaServicesCallError : tNextErrorBase
    {

        public TeknosaServicesCallError()
            : base("TEKNOSA_SERVICESS_ERROR", "Teknosa servisi çağrılırken hata meydana geldi", "Servis Hatası")
        {
        }


        public TeknosaServicesCallError(string code = null, string internalMessage = null, string externalMessage = null)
            : base($"TEKNOSA_SERVICESS_ERROR_{code}", internalMessage, externalMessage)
        {
        }
    }
}
