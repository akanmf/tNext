using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Model.Enums;

namespace tNext.Common.Core.Helpers
{
    public class PlatformHelper
    {
        public static PlatformType DetectPlatform()
        {
            return PlatformType.ANDROID;
        }
    }
}
