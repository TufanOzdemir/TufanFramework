using Microsoft.Extensions.Caching.Memory;
using System;

namespace TufanFramework.Core.Caching
{
    public class MemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheManager(IMemoryCache cache)
        {
            _cache = cache;
        }

        public void Remove(string key)
        {
            _cache.Remove(key);
        }

        public void ClearAll(string regionKey = null)
        {
            _cache.Dispose();
        }

        public T Set<T>(string key, T value) where T : class
        {
            var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(DateTimeOffset.MaxValue)
                    .SetSlidingExpiration(TimeSpan.MaxValue)
                    .SetPriority(CacheItemPriority.NeverRemove);

            _cache.Set(key, value, options);
            return value;
        }

        public T Set<T>(string key, T value, TimeSpan absoluteExpiration) where T : class
        {
            var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(absoluteExpiration)
                    .SetSlidingExpiration(TimeSpan.MaxValue)
                    .SetPriority(CacheItemPriority.NeverRemove);

            _cache.Set(key, value, options);
            return value;
        }

        public T Set<T>(string key, T value, DateTimeOffset dateTimeOffset) where T : class
        {
            var options = new MemoryCacheEntryOptions()
                    .SetAbsoluteExpiration(dateTimeOffset)
                    .SetSlidingExpiration(TimeSpan.MaxValue)
                    .SetPriority(CacheItemPriority.NeverRemove);

            _cache.Set(key, value, options);
            return value;
        }

        public T Set<T>(TimeSpan slidingExpiration, string key, T value) where T : class
        {
            var options = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(slidingExpiration)
                    .SetPriority(CacheItemPriority.NeverRemove);

            _cache.Set(key, value, options);
            return value;
        }

        public T GetObject<T>(string key) where T : class
        {
            _cache.TryGetValue(key, out T value);
            return value;
        }
    }
}