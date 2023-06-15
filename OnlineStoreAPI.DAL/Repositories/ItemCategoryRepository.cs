using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;
using System;
using System.Linq;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ItemCategoryRepository> _logger;

        public ItemCategoryRepository(AppDbContext db, ILogger<ItemCategoryRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<ItemCategory> AddPropertyAsync(ItemCategory data)
        {
            try
            {
                var result = await _db.ItemCategories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == data.Id);
                result.ItemProperty.AddRange(data.ItemProperty);
                _db.ItemCategories.Update(result);
                await _db.SaveChangesAsync();
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when add ItemCategory to item {data.Id}");
                throw ex;
            }
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
                var requst = await _db.ItemCategories
                    .AsNoTracking()
                    .Include(x => x.ItemProperty)
                    .FirstOrDefaultAsync(x => x.Id == id);

                var result = _db.ItemCategories
                    .Remove(requst);
                _db.ItemPropertis.RemoveRange(requst.ItemProperty);

                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete ItemCategory {id}");
                throw ex;
            }
        }

        public async Task<ItemCategory> DeletePropertyAsync(ItemCategory data)
        {
            try
            {
                var result = await _db.ItemCategories
                .Include(x => x.ItemProperty)
                .FirstOrDefaultAsync(x => x.Id == data.Id);

                result.ItemProperty.RemoveAll(x => data.ItemProperty.Any(e => e.Id == x.Id));

                _db.Entry(result).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete ItemCategory to item {data.Id}");
                throw ex;
            }
        }

        public async Task<ItemCategory> UpdatePropertyAsync(ItemCategory data)
        {
            try
            {
                var result = await _db.ItemCategories
                .Include(x => x.ItemProperty)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == data.Id);

                foreach (var property in data.ItemProperty)
                {
                    int index = result.ItemProperty.IndexOf(result.ItemProperty.Where(x => x.Id == property.Id).FirstOrDefault());
                    result.ItemProperty[index] = property;
                }

                _db.Entry(result).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete ItemCategory to item {data.Id}");
                throw ex;
            }
        }

        public async Task<ItemCategory> GetAsync(int id)
        {
            try
            {
                return await _db.ItemCategories
                    .Include(x => x.Category)
                    .Include(x => x.ItemProperty)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
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
                return await _db.ItemCategories.Include(x => x.Category).AsNoTracking().ToListAsync();
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
