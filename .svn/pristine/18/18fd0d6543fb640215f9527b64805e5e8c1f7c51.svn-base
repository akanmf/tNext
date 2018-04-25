using Couchbase;
using Couchbase.Configuration.Client;
using Couchbase.Core;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tNext.Common.Core.Helpers;
using tNext.Common.Core.Interfaces;

namespace tNext.Common.Core.Caching
{
    public class CouchbaseCacheManager : ICacheManager
    {
        static string CachePrefix { get; set; }

        static string GetKey(string key)
        {
            return key = $"{tNextMicroservice.Environment}_{key}_{CachePrefix}";
        }

        static IBucket bucket;

        static CouchbaseCacheManager()
        {
            CachePrefix = (new Random()).Next(10000).ToString();

            try
            {
                var couchbaseServer = ConfigurationHelper.GetConfiguration("CouchbaseServer");
                var clientConfiguration = new ClientConfiguration
                {
                    Servers = new List<Uri> { new Uri(couchbaseServer) },
                };

                ClusterHelper.Initialize(clientConfiguration);

                bucket = ClusterHelper.GetBucket("Teknosa");
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
                throw;
            }
        }

        public List<string> ClearAllCachedItems()
        {
            var response = new List<string>();
            CachePrefix = (new Random()).Next(10000).ToString();
            response.Add("OK");
            return response;
        }

        public string Get(string key)
        {
            key = GetKey(key);
            var document = bucket.GetDocument<string>(key);
            return document.Content;
        }

        public T Get<T>(string key)
        {
            key = GetKey(key);
            var document = bucket.GetDocument<T>(key);
            return document.Content;
        }

        public T GetOrSet<T>(string key, uint expiration, Func<T> objectSettingFunction)
        {
            key = GetKey(key);
            var document = Get<T>(key);
            if (document == null)
            {
                document = objectSettingFunction();
                Set<T>(key, document, expiration);
            }
            return document;
        }

        public void Set<T>(string key, T value)
        {
            key = GetKey(key);
            try
            {
                var document = new Document<T>
                {
                    Id = key,
                    Content = value
                };
                bucket.Upsert(document);
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
            }
        }

        public void SetAsync<T>(string key, T value)
        {
            key = GetKey(key);
            try
            {
                var document = new Document<T>
                {
                    Id = key,
                    Content = value
                };
                bucket.UpsertAsync(document);
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
            }
        }


        public void Set<T>(string key, T value, uint expiration)
        {
            key = GetKey(key);
            try
            {
                var document = new Document<T>
                {
                    Id = key,
                    Content = value,
                    Expiry = expiration * 1000
                };

                bucket.Upsert(document);
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
            }
        }

        public void SetAsync<T>(string key, T value, uint expiration)
        {
            key = GetKey(key);
            try
            {
                var document = new Document<T>
                {
                    Id = key,
                    Content = value,
                    Expiry = expiration * 1000
                };

                bucket.UpsertAsync(document);
            }
            catch (Exception ex)
            {
                tNextLogManager.LogError(ex);
            }
        }
    }
}
