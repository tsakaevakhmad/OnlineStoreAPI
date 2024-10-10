using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;
using System;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CategoryRepository> _logger;
        private readonly IRepositoryCacheServices _cacheServices;

        public CategoryRepository(AppDbContext db, ILogger<CategoryRepository> logger, IRepositoryCacheServices cacheServices) 
        {
            _db = db;
            _logger = logger;
            _cacheServices = cacheServices;
        }

        public async Task<Category> CreateAsync(Category data)
        {
            try
            {
                data = (await _db.Categories.AddAsync(data)).Entity;
                await _db.SaveChangesAsync();
                await _cacheServices.OnCreateAsync("categories", data, 1);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when try create item with name: \"{data.Name}\" in category repository");
                throw ex;
            }
        }

        public async Task<Category> DeleteAsync(int id)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var requst = await _db.Categories
                    .AsNoTracking()
                    .Include(x => x.ItemProperty)
                    .FirstOrDefaultAsync(x => x.Id == id);

                var result = _db.Categories
                    .Remove(requst);
                _db.ItemPropertis.RemoveRange(requst.ItemProperty);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                await _cacheServices.OnDeleteAsync<Category>(id.ToString(), "categories", 1, x => x.Id == id);
                await _cacheServices.DeleteAsync("items");
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete Category {id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<Category> GetAsync(int id)
        {
            try
            {
                Category category;
                category = await _cacheServices.OnGetAsync<Category>(id.ToString());
                if(category == null)
                {
                    category = await _db.Categories.Include(x => x.ItemProperty)
                        .AsNoTracking()
                        .FirstAsync(x => x.Id == id);
                    await _cacheServices.AddAsync(id.ToString(), category, 1);
                }
                return category;
            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, $"Error when try get by id with id: \"{id}\" in category repository");
                throw ex;
            }
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            try
            {
                List<Category> categories;
                categories = await _cacheServices.OnGetAsync<List<Category>>(nameof(categories));
                if (categories == null)
                {
                    categories = await _db.Categories.ToListAsync();
                    await _cacheServices.AddAsync(nameof(categories), categories, 1);
                }
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when try get all in category repository");
                throw ex;
            }
        }

        public async Task<Category> UpdateAsync(Category data)
        {
            try
            {
                var entity = _db.Entry<Category>(data);
                entity.State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await _cacheServices.OnUpdateAsync<Category>(data.Id.ToString(), "categories", entity.Entity, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync(data.Id.ToString());
                await _cacheServices.DeleteAsync("items");
                return entity.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when update Category {data.Id}");
                throw ex;
            }
        }

        public async Task<Category> AddPropertyAsync(Category data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == data.Id);
                result.ItemProperty.AddRange(data.ItemProperty);
                _db.Categories.Update(result);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.DeleteAsync(data.Id.ToString());
                await _cacheServices.DeleteAsync("items");
                await _cacheServices.DeleteAsync("categories");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when add Category to item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<Category> DeletePropertyAsync(Category data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.Categories
                .Include(x => x.ItemProperty)
                .FirstOrDefaultAsync(x => x.Id == data.Id);

                result.ItemProperty.RemoveAll(x => data.ItemProperty.Any(e => e.Id == x.Id));

                _db.Entry(result).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.OnUpdateAsync<Category>(data.Id.ToString(), "categories", result, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync("items");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete Category from item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<Category> UpdatePropertyAsync(Category data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.Categories
                .Include(x => x.ItemProperty)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == data.Id);
                //Нужно пофиксить
                foreach (var property in data.ItemProperty)
                {
                    int index = result.ItemProperty.IndexOf(result.ItemProperty.FirstOrDefault(x => x.Id == property.Id));
                    result.ItemProperty[index] = property;
                    var itemPropertyEntity = _db.Entry<ItemProperty>(result.ItemProperty[index]);
                    itemPropertyEntity.State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                await _cacheServices.OnUpdateAsync<Category>(data.Id.ToString(), "categories", result, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync("items");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete Category to item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }
    }
}
