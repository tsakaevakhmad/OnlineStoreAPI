using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Interfaces;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OnlineStoreAPI.DAL.RepositoryServices
{
    public class RepositoryCacheServices : IRepositoryCacheServices
    {
        private readonly IDistributedCache _cache;
        private readonly JsonSerializerOptions _jsonOptions;
        private readonly ILogger<RepositoryCacheServices> _logger;

        public RepositoryCacheServices(IDistributedCache cache, ILogger<RepositoryCacheServices> logger)
        {
            _cache = cache;
            _jsonOptions = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
            };
            _logger = logger;
        }

        public async Task AddAsync<T>(string key, T value, int minutes)
        {
            try
            {
                await _cache.SetStringAsync(key, JsonSerializer.Serialize(value, _jsonOptions), new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(minutes)
                });
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, nameof(AddAsync));
            }
        }

        public async Task OnCreateAsync<T>(string listKey, T value, int minutes)
        {
            
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(OnCreateAsync));
            }
        }

        public async Task OnDeleteAsync<T>(string key, string listKey, int minutes, Func<T, bool> predicate)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(OnDeleteAsync));
            }
        }

        public async Task OnDeleteAsync<T>(string key, string listKey)
        {
            try
            {
                await _cache.RemoveAsync(listKey);
                await _cache.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(OnDeleteAsync));
            }
        }

        public async Task DeleteAsync(string key)
        {
            try
            {
                await _cache.RemoveAsync(key);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(DeleteAsync));
            }
        }

        public async Task<T> OnGetAsync<T>(string key)
        {
            try
            {
                T result = default(T);
                var cacheResult = await _cache.GetStringAsync(key);
                if (cacheResult != null)
                {
                    result = JsonSerializer.Deserialize<T>(cacheResult, _jsonOptions);
                }
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(OnGetAsync));
            }
            return default(T);
        }

        public async Task<T> OnUpdateAsync<T>(string key, string listKey, T value, int minutes, Func<T, bool> predicate)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, nameof(OnUpdateAsync));
            }
            return default(T);
        }
    }
}
