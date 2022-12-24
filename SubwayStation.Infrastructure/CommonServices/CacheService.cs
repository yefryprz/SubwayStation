using Microsoft.Extensions.Caching.Memory;
using SubwayStation.Application.Contracts;
using System.Text.Json;

namespace SubwayStation.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        private readonly IMemoryCache _memoryCache;

        public CacheService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public bool GetCacheValue<T>(string key, ref T value)
        {
            var result = _memoryCache.Get(key);
            if (result is null) return false;
            value = JsonSerializer.Deserialize<T>(result.ToString());
            return value != null;
        }

        public async Task SetCacheValueAsync<T>(string key, T value, TimeSpan? expiration = null)
        {
            var values = await Task.FromResult(JsonSerializer.Serialize(value));
            _memoryCache.Set(key, values, new MemoryCacheEntryOptions { AbsoluteExpirationRelativeToNow = expiration });
        }
    }
}
