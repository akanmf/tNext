using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Model.Errors
{
    public class NotLoggedInError : tNextErrorBase
    {
        public NotLoggedInError() : base("NOT_LOGGED_IN", "Bu işlemi yapabilmeniz için login olmanız gerekmektedir", "Bu işlemi yapabilmek için önce giriş yapmanız gerekmektedir")
        {
        }

        public NotLoggedInError(string internalMessage = null, string externalMessage = null) : base("NOT_LOGGED_IN", internalMessage, externalMessage)
        {
        }
    }
}
