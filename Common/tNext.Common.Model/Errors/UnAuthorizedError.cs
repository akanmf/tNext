using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Model.Errors
{
    public class UnAuthorizedError : tNextErrorBase
    {
        public UnAuthorizedError() : base("UNAUTHORIZED", "yetkiniz bulunmamaktadır", "Yetkiniz bulunmamaktadır")
        {
        }

        public UnAuthorizedError(string internalMessage = null, string externalMessage = null) : base("UNAUTHORIZED", internalMessage, externalMessage)
        {
        }
    }
}
