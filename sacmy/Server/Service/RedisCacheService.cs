using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace sacmy.Server.Service
{
    public class RedisCacheService
    {
        private readonly IDistributedCache _cache;

        public RedisCacheService(IDistributedCache cache)
        {
            _cache = cache;
        }

        // الحصول على البيانات من التخزين المؤقت
        public async Task<T> GetCacheDataAsync<T>(string key)
        {
            var cachedData = await _cache.GetStringAsync(key);
            if (!string.IsNullOrEmpty(cachedData))
            {
                return JsonConvert.DeserializeObject<T>(cachedData);
            }
            return default;
        }

        // حفظ البيانات في التخزين المؤقت
        public async Task SetCacheDataAsync<T>(string cacheKey, T data, TimeSpan? expiration)
        {
            var cacheOptions = new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = expiration ?? TimeSpan.FromHours(8) // Set default expiration to 8 hours
            };

            var serializedData = JsonConvert.SerializeObject(data);
            await _cache.SetStringAsync(cacheKey, serializedData, cacheOptions);
        }


        // إزالة البيانات من التخزين المؤقت
        public async Task RemoveCacheDataAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }
    }
}
