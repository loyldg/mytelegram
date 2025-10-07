namespace MyTelegram.Caching.Redis
{
    public interface IRedisHelper
    {
        Task<bool> SetIfNotExistsAsync(string key, byte[] value, TimeSpan? expiry = null);
        Task<byte[]?> GetAsync(string key);
        Task<bool> DeleteAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task<long> IncrementAsync(string key, long value = 1);
    }
}
