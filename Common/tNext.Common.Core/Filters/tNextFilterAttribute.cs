using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using tNext.Common.Core.Model;
using tNext.Common.Model.Enums;
using tNext.Common.Model.Errors;

namespace tNext.Common.Core.Filters
{
    /// <summary>
    /// Request filter
    /// </summary>
    public class tNextFilterAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public tNextFilterAttribute()
        {
            AuthenticateApplication = true;
            AuthenticateOperator = false;
            AuthenticationCustomer = false;
        }

        /// <summary>
        /// Define if client required, operator required
        /// </summary>
        /// <param name="isApplicationRequired"></param>
        /// <param name="isOperatorRequired"></param>
        public tNextFilterAttribute(bool isApplicationRequired, bool isOperatorRequired) : this()
        {
            AuthenticateApplication = isApplicationRequired;
            AuthenticateOperator = isOperatorRequired;
        }

        /// <summary>
        /// Define if client required, operator required and customer required
        /// </summary>
        /// <param name="isApplicationRequired">Client required</param>
        /// <param name="isOperatorRequired">Operator required</param>
        /// <param name="isCustomerRequired">Customer required</param>
        public tNextFilterAttribute(bool isApplicationRequired, bool isOperatorRequired, bool isCustomerRequired) : this(isApplicationRequired, isOperatorRequired)
        {
            AuthenticationCustomer = isCustomerRequired;
        }

        /// <summary>
        /// Override
        /// </summary>
        /// <param name="actionContext"></param>
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            if (AuthenticateApplication)
            {
                string clientName = actionContext.Request.Headers.GetHeader(Headers.ClientName, "");
                if (clientName.IsNull())
                {
                    actionContext.Response = this.CreateUnAuthorizedResponse(actionContext, $"Missing header: {Headers.ClientName}");
                }
            }

            if (AuthenticateOperator)
            {
                string operatorId = actionContext.Request.Headers.GetHeader(Headers.OperatorId, "");
                if (operatorId.IsNull())
                {
                    actionContext.Response = this.CreateUnAuthorizedResponse(actionContext, $"Missing header: {Headers.OperatorId}");
                }
            }

            if (AuthenticationCustomer)
            {
                string customerId = actionContext.Request.Headers.GetHeader(Headers.CustomerId, "");
                if (customerId.IsNull())
                {
                    actionContext.Response = this.CreateUnAuthorizedResponse(actionContext, $"Missing header: {Headers.CustomerId}");
                }
            }

            string requestId = actionContext.Request.Headers.GetHeader(Headers.RequestId, "");
            if (string.IsNullOrEmpty(requestId))
            {
                actionContext.Response = this.CreateUnAuthorizedResponse(actionContext, $"Missing header: {Headers.RequestId}");
            }
        }

        private HttpResponseMessage CreateUnAuthorizedResponse(HttpActionContext actionContext, string internalMessage)
        {
            var exception = new UnAuthorizedError(internalMessage: internalMessage);
            return new tNextResponseCreator().IncludingError(exception).CreateResponse();
        }

        /// <summary>
        /// Is Client Required (default true)
        /// </summary>
        public bool AuthenticateApplication { get; private set; }
        /// <summary>
        /// Is Operator Required (default false)
        /// </summary>
        public bool AuthenticateOperator { get; private set; }
        /// <summary>
        /// Is Customer Required (default false)
        /// </summary>
        public bool AuthenticationCustomer { get; private set; }
    }
}
