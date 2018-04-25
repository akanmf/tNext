using Newtonsoft.Json;
using System.Collections.Generic;
using tNext.Common.Model;

namespace tNext.Common.Core.Helpers
{
    public class ParameterHelper
    {
        public static List<ParameterItem> GetParameter(string group, string key = "")
        {
            var url = $"{ ConfigurationHelper.GetConfiguration("tNext.Microservices.Parameter.Api.Url")}?group={group}&key={key}";
            var restCall = new Common.Core.Rest.RestCall(url).Get();
            var result = restCall.SendAndGetResponse();
            var parameterList = JsonConvert.DeserializeObject<List<ParameterItem>>(result.Data.ToString());
            return parameterList;
        }
    }
}
