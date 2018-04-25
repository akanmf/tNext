using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;
using tNext.Common.Model.Errors;

namespace tNext.Common.Core.Filters
{
    public class RequestFilter : ActionFilterAttribute
    {
        public bool CustomerRequired { get; set; }

        public RequestFilter()
        {
            CustomerRequired = false;
        }

        public RequestFilter(bool customerRequired)
        {
            CustomerRequired = customerRequired;
        }


        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (CustomerRequired)
            {
                if (tNextExecutionContext.Current.Customer == null)
                {
                    NotLoggedInError err = new NotLoggedInError();
                    actionContext.Response = tNextResponseCreator.NOK(err);
                    return;
                }
            }

            base.OnActionExecuting(actionContext);
        }
    }
}
