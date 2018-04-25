using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Model
{
    public class ParameterItem : BaseEntity
    {
        public virtual string Group { get; set; }

        public virtual string Key { get; set; }

        public virtual string Value { get; set; }

        public virtual string Code1 { get; set; }

        public virtual string Code2 { get; set; }

        public virtual string Code3 { get; set; }

        public virtual string Description{ get; set; }
    }
}
