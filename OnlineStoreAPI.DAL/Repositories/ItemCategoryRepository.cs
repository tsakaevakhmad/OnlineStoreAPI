using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemCategoryRepository : IRepository<ItemCategory>
    {
        private readonly AppDbContext _db;

        public ItemCategoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<ItemCategory> CreateAsync(ItemCategory data)
        {
            throw new NotImplementedException();
        }

        public Task<ItemCategory> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemCategory> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemCategory>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemCategory> UpdateAsync(ItemCategory data)
        {
            throw new NotImplementedException();
        }
    }
}
