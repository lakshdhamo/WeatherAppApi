using Microsoft.Extensions.Caching.Memory;

namespace WeatherApp.Domain.Interfaces
{
    public interface ICacheManager
    {
        /// <summary>
        /// Adds the Cache value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="vlaue"></param>
        /// <param name="memoryCacheEntryOptions"></param>
        void Add<T>(string key, T vlaue, MemoryCacheEntryOptions memoryCacheEntryOptions);

        /// <summary>
        /// Gets the cached value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="val"></param>
        /// <returns></returns>
        T Get<T>(string key, out T val);

        /// <summary>
        /// Remove the cached value
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}