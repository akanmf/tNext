using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Caching;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Core.Interfaces;

namespace tNext.Common.Core.Caching
{
    public class LocalCacheManager : ICacheManager
    {
        public T GetOrSet<T>(string key, uint expiration, Func<T> objectSettingFunction)
        {

            var cachedObject = (T)MemoryCache.Default[key];
            if (cachedObject == null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expiration);
                cachedObject = objectSettingFunction();
                if (cachedObject != null)
                {
                    MemoryCache.Default.Set(key, cachedObject, policy);
                }
            }
            return cachedObject;
        }


        public string Get(string key)
        {
            return Get<string>(key);
        }

        public T Get<T>(string key)
        {
            var cachedObject = (T)MemoryCache.Default[key];
            return cachedObject;
        }

        public void Set<T>(string key, T value)
        {
            Set(key, value, 60 * 60);
        }

        public void Set<T>(string key, T value, uint expiration)
        {
            CacheItemPolicy policy = new CacheItemPolicy();
            policy.AbsoluteExpiration = DateTimeOffset.Now.AddSeconds(expiration);
            MemoryCache.Default.Set(key, value, policy);
        }

        public List<string> ClearAllCachedItems()
        {
            var keys = MemoryCache.Default.Select(s => s.Key).ToList();
            foreach (var key in keys)
            {
                MemoryCache.Default.Remove(key);
                tNextLogManager.Debug(key);
            }

            return keys;
        }
    }
}
