namespace tNext.ApiGateway.Api.Helpers
{
    public class RequestHelper
    {
        public static string GetRequestedMethodName()
        {
            var requestedMethodArray = tNextExecutionContext.Current.RequestUri.Split('/');
            var requestedMethodName = requestedMethodArray[requestedMethodArray.Length - 1].Split('?')[0];

            return requestedMethodName;
        }
    }
}