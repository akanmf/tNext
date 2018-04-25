using tNext.Common.Core.Model;
using tNext.Common.Model.Abstracts;

namespace tNext.ApiGateway
{
    public static class tNextResponseExtensions
    {
        public static void MaskForClientApplications(this tNextResponse input)
        {
            if (!tNextExecutionContext.Current.TraceEnabled && input.Error != null)
            {
                //TODO: Burayı productiona çıkıldığında açmayı unutma
                //input.Error.InternalMessage = string.Empty;
            }
        }

        public static tNextErrorBase MaskForClientApplications(this tNextErrorBase input)
        {
            if (!tNextExecutionContext.Current.TraceEnabled && input != null)
            {
                //TODO: Burayı productiona çıkıldığında açmayı unutma
                //input.InternalMessage = string.Empty;
            }

            return input;
        }
    }
}