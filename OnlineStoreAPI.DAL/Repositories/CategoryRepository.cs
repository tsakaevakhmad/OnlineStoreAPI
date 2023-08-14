using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;
using System;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class CategoryRepository : IRepository<Category>
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
            try
            {
                var data = await _db.Categories.FindAsync(id);
                _db.Categories.Remove(data);
                await _db.SaveChangesAsync();
                await _cacheServices.OnDeleteAsync<Category>(id.ToString(), "categories", 1, x => x.Id == id);
                return data;
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, $"Error when try delete item with id: \"{id}\" in category repository");
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
                    category = await _db.Categories.FindAsync(id);
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
                return entity.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when try update item with id: \"{data.Id}\" in category repository");
                throw ex;
            }
        }
    }
}
