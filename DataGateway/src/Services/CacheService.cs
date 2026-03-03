using DataGateway.Services.Interfaces;
using StackExchange.Redis;

namespace DataGateway.Services
{
    public class CacheService: ICacheService
    {

        private readonly IDatabase _dbCache;


        public CacheService(IConnectionMultiplexer connection)
        {
            _dbCache = connection.GetDatabase();
        }

        public async Task<string?> GetStringAsync(string key)
        {
            var res = await _dbCache.StringGetAsync(key);
            return res;
        }
        public async Task SetAsync(string key, string value, TimeSpan? expiry = null)
        {
            await _dbCache.StringSetAsync(key, value, expiry);
        }

        public async Task RemoveAsync(string key)
        {
            await _dbCache.KeyDeleteAsync(key);
        }

    }
}
