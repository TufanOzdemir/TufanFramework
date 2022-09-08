using System;

namespace TufanFramework.Core.Caching
{
    public interface ICacheManager
    {
        /// <summary>
        /// Remove cache by key
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);

        /// <summary>
        /// Remove all cache
        /// </summary>
        /// <param name="regionKey">Boş gelirse tüm cache'i, dolu gelirse belirlenen region'ı siler.</param>
        void ClearAll(string regionKey = null);

        /// <summary>
        /// Add value from cache
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        T Set<T>(string key, T value) where T : class;

        /// <summary>
        /// Time over -> remove cache with Item use
        /// Item kullanılsa dahi süre sıfırlanmaz süre bitince silinir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="absoluteExpiration"></param>
        /// <returns></returns>
        T Set<T>(string key, T value, TimeSpan absoluteExpiration) where T : class;

        /// <summary>
        /// GetCache
        /// Verilen tipteki objeyi verilen anahtar ile getirir.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetObject<T>(string key) where T : class;
    }
}