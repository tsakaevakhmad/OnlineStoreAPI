using Microsoft.Extensions.Caching.Distributed;
using OnlineStoreAPI.DAL.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineStoreAPI.DAL.RepositoryServices
{
    public class RepositoryCacheServices : IRepositoryCacheServices
    {
        private readonly IDistributedCache _cache;
        private readonly JsonSerializerOptions _jsonOptions;

        public RepositoryCacheServices(IDistributedCache cache)
        {
            _cache = cache;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
        }

        public async Task AddAsync<T>(string key, T value, int minutes)
        {
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
            });
        }

        public async Task OnCreateAsync<T>(string listKey, T value, int minutes)
        {
            var cacheResult = await _cache.GetStringAsync(listKey);
            if (cacheResult != null)
            {
                var result = JsonSerializer.Deserialize<List<T>>(cacheResult, _jsonOptions);
                result.Add(value);
                await _cache.SetStringAsync(listKey, JsonSerializer.Serialize(result, _jsonOptions), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
                });
            }
        }

        public async Task OnDeleteAsync<T>(string key, string listKey, int minutes, Func<T, bool> predicate)
        {
            var cacheResult = await _cache.GetStringAsync(listKey);
            if (cacheResult != null)
            {
                var resultList = JsonSerializer.Deserialize<List<T>>(cacheResult, _jsonOptions);
                var itemToDelete = resultList.FirstOrDefault(item => predicate(item));

                if (itemToDelete != null)
                {
                    resultList.Remove(itemToDelete);

                    await _cache.SetStringAsync(listKey, JsonSerializer.Serialize(resultList, _jsonOptions), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
                    });
                }
            }
            await _cache.RemoveAsync(key);
        }

        public async Task OnDeleteAsync<T>(string key, string listKey)
        {
            await _cache.RemoveAsync(listKey);
            await _cache.RemoveAsync(key);
        }

        public async Task DeleteAsync(string key)
        {
            await _cache.RemoveAsync(key);
        }

        public async Task<T> OnGetAsync<T>(string key)
        {
            T result = default(T);
            var cacheResult = await _cache.GetStringAsync(key);
            if (cacheResult != null)
            {
                result = JsonSerializer.Deserialize<T>(cacheResult, _jsonOptions);
            }
            return result;
        }

        public async Task<T> OnUpdateAsync<T>(string key, string listKey, T value, int minutes, Func<T, bool> predicate)
        {
            var cacheResult = await _cache.GetStringAsync(listKey);
            if (cacheResult != null)
            {
                var resultList = JsonSerializer.Deserialize<List<T>>(cacheResult, _jsonOptions);
                var itemToUpdate = resultList.FirstOrDefault(item => predicate(item));
                if (itemToUpdate != null)
                {
                    resultList.Remove(itemToUpdate);
                    itemToUpdate = value;
                    resultList.Add(itemToUpdate);
                    await _cache.SetStringAsync(listKey, JsonSerializer.Serialize(resultList, _jsonOptions), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
                    });
                }
            }
            await _cache.SetStringAsync(key, JsonSerializer.Serialize(value, _jsonOptions), new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
            });
            return value;
        }
    }
}
