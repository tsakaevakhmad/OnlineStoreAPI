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

        public CategoryRepository(AppDbContext db, ILogger<CategoryRepository> logger) 
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Category> CreateAsync(Category data)
        {
            try
            {
                await _db.Categories.AddAsync(data);
                await _db.SaveChangesAsync();
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
                return await _db.Categories.FindAsync(id); ;
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
                return await _db.Categories.ToListAsync();
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
