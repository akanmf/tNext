using System.Configuration;
using tNext.Common.Model.Abstracts;

namespace tNext.Common.Model.Errors
{
    public class UnderConstructionError : tNextErrorBase
    {
        public UnderConstructionError()
            : base("UNDER_CONSTRUCTION", ConfigurationManager.AppSettings["UnderConstructionText"], ConfigurationManager.AppSettings["UnderConstructionText"])
        {
        }


        public UnderConstructionError(string code = null, string internalMessage = null, string externalMessage = null)
            : base($"UNDER_CONSTRUCTION{code}", internalMessage, externalMessage)
        {
        }
    }
}
