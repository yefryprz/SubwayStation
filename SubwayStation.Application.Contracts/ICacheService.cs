namespace SubwayStation.Application.Contracts
{
    public interface ICacheService
    {
        Task SetCacheValueAsync<T>(string key, T value, System.TimeSpan? expiration = null);
        void DelCacheValue(string key);
        bool GetCacheValue<T>(string key, ref T value);
    }
}
