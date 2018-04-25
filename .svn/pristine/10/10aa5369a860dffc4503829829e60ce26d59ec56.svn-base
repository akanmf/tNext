using System;
using NUnit.Framework;
using tNext.Common.Core.Helpers;

namespace tNext.Common.UnitTest
{
    [TestFixture]
    public class CacheHelperTests
    {
        string testKey = "TEST-KEY";
        string testValue = "Cache test value";

        public CacheHelperTests()
        {
            CacheHelper.Local.Set(testKey, testValue);
            CacheHelper.Remote.Set(testKey, testValue);
        }

        [Test]
        public void LocalCacheSet()
        {
            string strToCache = "Bunu cache'e at";
            string key = "TEST-" + Guid.NewGuid().ToString();

            CacheHelper.Local.Set(key, strToCache);

            Assert.Pass();
        }


        [TestCase("TEST-KEY", ExpectedResult = true)]
        [TestCase("NOT_EXIST-TEST-KEY", ExpectedResult = false)]
        public bool LocalCacheGet(string key)
        {
            var cachedStr = CacheHelper.Local.Get(key);
            return testValue == cachedStr;
        }

        [Test]
        public void LocalCacheGetAndSet()
        {
            string key = "TEST-" + Guid.NewGuid().ToString();

            var cachedString = CacheHelper.Local.GetOrSet(key, 10, () => { return "Bunu cache'e at"; });

            Assert.AreEqual("Bunu cache'e at", cachedString);
        }




        [Test]
        public void RemoteCacheSet()
        {
            string strToCache = "Bunu cache'e at";
            string key = "TEST-" + Guid.NewGuid().ToString();

            CacheHelper.Remote.Set(key, strToCache);

            Assert.Pass();
        }


        [TestCase("TEST-KEY", ExpectedResult = true)]
        [TestCase("NOT_EXIST-TEST-KEY", ExpectedResult = false)]
        public bool RemoteCacheGet(string key)
        {
            var cachedStr = CacheHelper.Remote.Get(key);
            return testValue == cachedStr;
        }

        [Test]
        public void RemoteCacheGetAndSet()
        {
            string key = "TEST-" + Guid.NewGuid().ToString();

            var cachedString = CacheHelper.Remote.GetOrSet(key, 10, () => { return "Bunu cache'e at"; });

            Assert.AreEqual("Bunu cache'e at", cachedString);
        }

    }
}
