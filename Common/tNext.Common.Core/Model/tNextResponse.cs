using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Core.Model
{
    [Serializable]
    public class tNextResponse
    {
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }

        [JsonProperty("error")]
        public tNextErrorBase Error { get; set; }

        [JsonProperty("data")]
        public object Data { get; set; }
    }
}
