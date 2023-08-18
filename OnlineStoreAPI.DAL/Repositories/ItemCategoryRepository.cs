using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemCategoryRepository : IItemCategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ItemCategoryRepository> _logger;
        private readonly IRepositoryCacheServices _cacheServices;

        public ItemCategoryRepository(AppDbContext db, ILogger<ItemCategoryRepository> logger, IRepositoryCacheServices cacheServices)
        {
            _db = db;
            _logger = logger;
            _cacheServices = cacheServices;
        }

        public async Task<ItemCategory> AddPropertyAsync(ItemCategory data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.ItemCategories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == data.Id);
                result.ItemProperty.AddRange(data.ItemProperty);
                _db.ItemCategories.Update(result);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.DeleteAsync(data.Id.ToString());
                await _cacheServices.DeleteAsync("items");
                await _cacheServices.DeleteAsync("itemcategories");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when add ItemCategory to item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<ItemCategory> CreateAsync(ItemCategory data)
        {
            try
            {
                var result = await _db.ItemCategories.AddAsync(data);
                await _db.SaveChangesAsync();
                await _cacheServices.OnCreateAsync("itemcategories", result, 1);
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
            var transaction = await _db.Database.BeginTransactionAsync();
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
                await transaction.CommitAsync();

                await _cacheServices.OnDeleteAsync<ItemCategory>(id.ToString(), "itemcategories", 1, x => x.Id == id);
                await _cacheServices.DeleteAsync("items");
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete ItemCategory {id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<ItemCategory> DeletePropertyAsync(ItemCategory data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.ItemCategories
                .Include(x => x.ItemProperty)
                .FirstOrDefaultAsync(x => x.Id == data.Id);

                result.ItemProperty.RemoveAll(x => data.ItemProperty.Any(e => e.Id == x.Id));

                _db.Entry(result).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.OnUpdateAsync<ItemCategory>(data.Id.ToString(), "itemcategories", result, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync("items");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete ItemCategory from item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<ItemCategory> UpdatePropertyAsync(ItemCategory data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.ItemCategories
                .Include(x => x.ItemProperty)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == data.Id);
                //Нужно пофиксить
                foreach (var property in data.ItemProperty)
                {
                    int index = result.ItemProperty.IndexOf(result.ItemProperty.FirstOrDefault(x => x.Id == property.Id));
                    result.ItemProperty[index] = property;
                }
                var entity = _db.Entry<ItemCategory>(result);
                entity.State = EntityState.Modified;

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                await _cacheServices.OnUpdateAsync<ItemCategory>(data.Id.ToString(), "itemcategories", result, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync("items");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete ItemCategory to item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<ItemCategory> GetAsync(int id)
        {
            try
            {
                var itemCategory = await _cacheServices.OnGetAsync<ItemCategory>(id.ToString());
                if (itemCategory == null)
                {
                    itemCategory = await _db.ItemCategories
                    .Include(x => x.Category)
                    .Include(x => x.ItemProperty)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                    await _cacheServices.AddAsync(id.ToString(), itemCategory, 1);
                }
                return itemCategory;
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
                var itemCategories = await _cacheServices.OnGetAsync<IEnumerable<ItemCategory>>("itemcategories");
                if (itemCategories == null)
                {
                    itemCategories = await _db.ItemCategories.Include(x => x.Category).AsNoTracking().ToListAsync();
                    await _cacheServices.AddAsync("itemcategories", itemCategories, 1);
                }
                return itemCategories;
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
                await _cacheServices.OnUpdateAsync<ItemCategory>(data.Id.ToString(), "itemcategories", entity.Entity, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync(data.Id.ToString());
                await _cacheServices.DeleteAsync("items");
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
