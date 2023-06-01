using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemRepository : IItemRepositories
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ItemRepository> _logger;

        public ItemRepository(AppDbContext db, ILogger<ItemRepository> logger) 
        { 
            _db = db; 
            _logger = logger;
        }

        public async Task<Item> CreateAsync(Item data)
        {
            try
            {
                
                var result = await _db.Items.AddAsync(data);

                await UpdatePriceHistoryAsync(data);

                await _db.ItemProperyValues.AddRangeAsync(data.ItemProperyValue);
                await _db.SaveChangesAsync();
                return data;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, $"Error when create item {data.Title}");
                throw ex;
            }
        }

        public async Task<ItemProperty> CreatePropertyAsync(ItemProperty data)
        {
            try
            {
                await _db.ItemPropertis.AddAsync(data);
                await _db.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when create item property {data.Name}");
                throw ex;
            }
        }

        public async Task<Item> DeleteAsync(int id)
        {
            try
            {
                var result = _db.Items.Remove(await _db.Items.FindAsync(id));
                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete item {id}");
                throw ex;
            }
        }

        public async Task<Item> GetAsync(int id)
        {
            try
            {
                return await _db.Items
                    .Include(x => x.ItemPriceHistories)
                    .Include(x => x.ItemCategory)
                    .Include(x => x.Company)
                    .Include(x => x.ItemProperyValue)
                    .ThenInclude(x => x.ItemProperty)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get item by id {id}");
                throw ex;
            }
        }

        public async Task<IEnumerable<Item>> GetAsync()
        {
            try
            {
                return await _db.Items.ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get all items ");
                throw ex;
            }
        }

        public async Task<Item> UpdateAsync(Item data)
        {
            try
            {
                var item = _db.Entry<Item>(data);
                item.State = EntityState.Modified;

                foreach(var itemProperyValues in data.ItemProperyValue)
                {
                    var values = _db.Entry<ItemProperyValue>(itemProperyValues);
                    values.State = EntityState.Modified;
                }

                await UpdatePriceHistoryAsync(data);

                await _db.SaveChangesAsync();
                return item.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when update item {data.Title}");
                throw ex;
            }
        }

        private async Task UpdatePriceHistoryAsync(Item data)
        {
            try
            {
                await _db.ItemPriceHistories.AddAsync(new ItemPriceHistory { Item = data, Price = (decimal)data.Price });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
