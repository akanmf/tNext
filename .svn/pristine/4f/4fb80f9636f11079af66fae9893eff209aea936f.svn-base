using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Core.Caching;
using tNext.Common.Core.Interfaces;

namespace tNext.Common.Core.Helpers
{
    public static class CacheHelper
    {
        static CacheHelper()
        {
            Remote = new CouchbaseCacheManager();
            Local = new LocalCacheManager();
        }
        
        public static ICacheManager Remote;
        
        public static ICacheManager Local;
    }
}
