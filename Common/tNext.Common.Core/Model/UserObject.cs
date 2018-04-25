using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Core.Model
{
    /// <summary>
    /// Model of [MSCS_Profiles].[dbo].[UserObject]
    /// </summary>
    public class UserObject
    {
        public string g_user_id { get; set; }
        public string u_first_name { get; set; }
        public string u_last_name { get; set; }
        public string u_email_address { get; set; }
        public string u_ceptel_extension { get; set; }
        public string u_ceptel_number { get; set; }

        public string FullName
        {
            get
            {
                return $"{u_first_name} {u_last_name}";
            }
        }
    }
}
