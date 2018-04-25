using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model;

namespace tNext.Common.Core.Helpers
{
    public class TeknosaConfigurationHelper
    {
        public static TeknosaConfigurationItem GetTeknosaConfiguration(string code)
        {
            var confSrvUrl = ConfigurationHelper.GetConfiguration("tNext.Microservices.Configuration.Api.Url");
            var restCall = new Common.Core.Rest.RestCall($"{confSrvUrl}/TeknosaConfiguration/{code}").Get();
            var result = restCall.SendAndGetResponse();
            var teknosaConf = JsonConvert.DeserializeObject<TeknosaConfigurationItem>(result.Data.ToString());
            return teknosaConf;
        }
    }
}
