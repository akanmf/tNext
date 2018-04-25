using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Model
{
    public abstract class BaseEntity
    {
        [JsonIgnore]
        public virtual long Id { get; set; }

        [JsonIgnore]
        public virtual string CreateChannel { get; set; }

        [JsonIgnore]
        public virtual DateTime CreateDate { get; set; }

        [JsonIgnore]
        public virtual string LastUpdateChannel { get; set; }

        [JsonIgnore]
        public virtual DateTime LastUpdateDate { get; set; }
    }
}
