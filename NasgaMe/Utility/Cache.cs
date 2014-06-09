using System;
using System.Text;
using System.Runtime.Caching;
using PostSharp.Aspects;

namespace NasgaMe.Utility
{
    public static class Cache
    {
        private static ObjectCache _cache
        {
            get { return MemoryCache.Default; }
        }

        #region CacheExpirations
        public static CacheItemPolicy OneDay = new CacheItemPolicy { AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration, Priority = CacheItemPriority.NotRemovable, SlidingExpiration = new TimeSpan(24, 0, 0) };
        public static CacheItemPolicy OneHour = new CacheItemPolicy { AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration, Priority = CacheItemPriority.NotRemovable, SlidingExpiration = new TimeSpan(1, 0, 0) };
        public static CacheItemPolicy FiveMinutes = new CacheItemPolicy { AbsoluteExpiration = ObjectCache.InfiniteAbsoluteExpiration, Priority = CacheItemPriority.NotRemovable, SlidingExpiration = new TimeSpan(0, 5, 0) };
        #endregion

        public static object Get(string key)
        {
            if (_cache.Contains(key)) return _cache.Get(key);
            return null;
        }

        public static void Set(string key, object value)
        {
            _cache.Set(key, value, OneHour);
        }

        public static void Remove(string key)
        {
            if (_cache.Contains(key)) _cache.Remove(key);
        }
        public static string GenerateKey(params object[] paramList)
        {
            var key = new StringBuilder();
            foreach (object obj in paramList)
                key.AppendFormat("_{0}_{1}", obj.GetType().Name, obj);
            return key.ToString();
        }

        public static string GenerateKey(Arguments arguments)
        {
            return GenerateKey(arguments.ToArray());
        }
    }
}