using StackExchange.Redis;

namespace MyTelegram.Caching.Redis
{
    public class RedisHelper : IRedisHelper
    {
        private readonly IDatabase _db;

        public RedisHelper(IConnectionMultiplexer multiplexer)
        {
            _db = multiplexer.GetDatabase();
        }

        /// <summary>
        /// Atomically sets the key only if it does not already exist (SET NX)
        /// </summary>
        public async Task<bool> SetIfNotExistsAsync(string key, byte[] value, TimeSpan? expiry = null)
        {
            return await _db.StringSetAsync(
                key: key,
                value: value,
                expiry: expiry,
                when: When.NotExists // NX
            ).ConfigureAwait(false);
        }

        public async Task<byte[]?> GetAsync(string key)
        {
            var val = await _db.StringGetAsync(key).ConfigureAwait(false);
            return val.HasValue ? (byte[]?)val! : null;
        }

        public async Task<bool> DeleteAsync(string key)
        {
            return await _db.KeyDeleteAsync(key).ConfigureAwait(false);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _db.KeyExistsAsync(key).ConfigureAwait(false);
        }

        public async Task<long> IncrementAsync(string key, long value = 1)
        {
            return await _db.StringIncrementAsync(key, value).ConfigureAwait(false);
        }
    }
}
