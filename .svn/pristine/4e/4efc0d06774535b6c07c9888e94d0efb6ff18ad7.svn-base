using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tNext.Common.Core.Interfaces
{
    public interface ICacheManager
    {

        List<string> ClearAllCachedItems();

        /// <summary>
        /// Gets a data from cache.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string Get(string key);

        /// <summary>
        /// Gets a data from cache as specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);


        /// <summary>
        /// Get object from the cache. If cache is empty, fills the cache with the given function
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="expiration">Expiration in seconds</param>
        /// <param name="objectSettingFunction">Function too fill the cache</param>
        /// <returns></returns>
        T GetOrSet<T>(string key, uint expiration, Func<T> objectSettingFunction);

        /// <summary>
        /// Set items as objects
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">Value of key</param>
        void Set<T>(string key, T value);

        /// <summary>
        /// Set item as object with expire duration
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">Value of key</param>
        /// <param name="expiration">Expire duration in seconds</param>
        void Set<T>(string key, T value, uint expiration);


    }
}
