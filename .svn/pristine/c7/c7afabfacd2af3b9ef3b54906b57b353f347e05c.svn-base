using Newtonsoft.Json;
using System;
using tNext.Common.Model.Interfaces;

namespace tNext.Common.Model.Abstracts
{
    [Serializable]
    public class tNextErrorBase : IBsml
    {
        [JsonProperty("code")]
        public string Code { get; set; }

        [JsonProperty("internalMessage")]
        public string InternalMessage { get; set; }

        [JsonProperty("externalMessage")]
        public string ExternalMessage { get; set; }

        public tNextErrorBase()
        {

        }

        protected tNextErrorBase(string code = null, string internalMessage = null, string externalMessage = null)
        {
            Code = code ?? Code;
            InternalMessage = internalMessage ?? InternalMessage;
            ExternalMessage = externalMessage ?? ExternalMessage;
        }
    }
}
