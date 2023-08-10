using Microsoft.Extensions.Caching.Distributed;
using OnlineStoreAPI.DAL.Interfaces;
using System.Text.Json;

namespace OnlineStoreAPI.DAL.RepositoryServices
{
    public class RepositoryCacheServices : IRepositoryCacheServices
    {
        private readonly IDistributedCache _cache;

        public RepositoryCacheServices(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task<bool> AddAsync<T>(string key, T value, int minutes)
        {
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
            });
            return true;
        }

        public async Task<bool> OnCreateAsync<T>(string key, T value, int minutes)
        {
            List<T> result = null;
            var cacheResult = await _cache.GetStringAsync(key);
            if (cacheResult != null)
            {
                result = JsonSerializer.Deserialize<List<T>>(cacheResult);
                result.Add(value);
                await _cache.SetStringAsync(key, JsonSerializer.Serialize(result), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
                });

                return true;
            }
            return false;
        }

        public async Task OnDeleteAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task<T> OnGetAsync<T>(string key)
        {
            T result = default(T);
            var cacheResult = await _cache.GetStringAsync(key);
            if (cacheResult != null)
            {
                result = JsonSerializer.Deserialize<T>(cacheResult);
            }
            return result;
        }

        public Task<T> OnUpdateAsync<T>(string key, T value, int minutes)
        {
            throw new NotImplementedException();
        }
    }
}
