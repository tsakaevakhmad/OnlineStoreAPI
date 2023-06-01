using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemRepository : IItemRepositories
    {
        private readonly AppDbContext _db;

        public ItemRepository(AppDbContext db) 
        { 
            _db = db; 
        }

        public async Task<Item> CreateAsync(Item data)
        {
            try
            {
                await _db.Items.AddAsync(data);
                await _db.ItemPriceHistories.AddAsync(new ItemPriceHistory { Item = data, Price = (decimal)data.Price });
                await _db.ItemProperyValues.AddRangeAsync(data.ItemProperyValue);
                await _db.SaveChangesAsync();
                return data;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ItemProperty> CreateProperty(ItemProperty data)
        {
            try
            {
                await _db.ItemPropertis.AddAsync(data);
                await _db.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Task<Item> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Item> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Item>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Item> UpdateAsync(Item data)
        {
            throw new NotImplementedException();
        }
    }
}
