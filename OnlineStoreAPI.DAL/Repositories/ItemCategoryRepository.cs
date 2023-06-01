using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemCategoryRepository : IRepository<ItemCategory>
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ItemCategoryRepository> _logger;

        public ItemCategoryRepository(AppDbContext db, ILogger<ItemCategoryRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ItemCategory> CreateAsync(ItemCategory data)
        {
            try
            {
                var result = await _db.ItemCategories.AddAsync(data);
                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when create ItemCategory {data.Name}");
                throw ex;
            }
        }

        public async Task<ItemCategory> DeleteAsync(int id)
        {
            try
            {
                var result = _db.ItemCategories.Remove(await _db.ItemCategories.FindAsync(id));
                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete ItemCategory {id}");
                throw ex;
            }
        }

        public async Task<ItemCategory> GetAsync(int id)
        {
            try
            {
                return await _db.ItemCategories.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get by id ItemCategory {id}");
                throw ex;
            }
        }

        public async Task<IEnumerable<ItemCategory>> GetAsync()
        {
            try
            {
                return await _db.ItemCategories.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get all ItemCategory");
                throw ex;
            }
        }

        public async Task<ItemCategory> UpdateAsync(ItemCategory data)
        {
            try
            {
                var entity = _db.Entry<ItemCategory>(data);
                entity.State = EntityState.Modified;
                await _db.SaveChangesAsync();
                return entity.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when update ItemCategory {data.Id}");
                throw ex;
            }
        }
    }
}
