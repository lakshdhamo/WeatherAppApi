using Microsoft.Extensions.Caching.Memory;
using System.Collections;
using System.Reflection;
using WeatherApp.Domain.Interfaces;

namespace WeatherApp.Domain.ServiceManager.CachingManager
{
    public class InMemoryCacheManager : ICacheManager
    {
        private readonly IMemoryCache _memoryCache;
        private static readonly Func<MemoryCache, object> GetEntriesCollection = Delegate.CreateDelegate(
        typeof(Func<MemoryCache, object>),
        typeof(MemoryCache).GetProperty("EntriesCollection", BindingFlags.NonPublic | BindingFlags.Instance).GetGetMethod(true),
        throwOnBindFailure: true) as Func<MemoryCache, object>;

        public InMemoryCacheManager(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        /// <summary>
        /// Adds the Cache value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vlaue"></param>
        /// <param name="memoryCacheEntryOptions"></param>
        public void Add<T>(string key, T value, MemoryCacheEntryOptions cacheOptions)
        {
            _memoryCache.Set(key, value, cacheOptions);
        }

        /// <summary>
        /// Gets the cached value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        public T Get<T>(string key, out T val)
        {
            if (_memoryCache.TryGetValue(key, out val))
            {
                return val;
            }
            return val;
        }

        /// <summary>
        /// Remove the cached value
        /// </summary>
        /// <param name="key"></param>
        public void Remove(string key)
        {
            if (((IDictionary)GetEntriesCollection((MemoryCache)_memoryCache)).Contains(key))
            {
                _memoryCache.Remove(key);
            }
        }
    }
}
