using Microsoft.EntityFrameworkCore;
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

                await UpdatePriceHistory(data);

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
                throw ex;
            }
        }

        public async Task<Item> GetAsync(int id)
        {
            try
            {
                return await _db.Items.FindAsync(id);
            }
            catch (Exception ex)
            {
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

                await _db.SaveChangesAsync();
                return item.Entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private async Task UpdatePriceHistory(Item data)
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
