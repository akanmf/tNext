using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Core.Helpers
{
    public static class DynamicHelper
    {
        /// <summary>
        /// Nullable Parameter Validation
        /// </summary>
        /// <param name="dynObj"></param>
        /// <param name="name"></param>
        /// <returns>True/False</returns>
        public static bool IsPropertyExist(dynamic dynObj, string name)
        {
            var members = (IEnumerable<string>)dynObj.GetDynamicMemberNames();
            return members.Contains(name);
        }
    }
}
