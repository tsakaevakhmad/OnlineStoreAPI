using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemRepository : IRepository<Item>
    {
        private readonly AppDbContext _db;

        public ItemRepository(AppDbContext db) 
        { 
            _db = db; 
        }

        public Task<Item> CreateAsync(Item data)
        {
            throw new NotImplementedException();
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
