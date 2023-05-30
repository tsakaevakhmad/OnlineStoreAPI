using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemPriceHistoryRepository : IRepository<ItemPriceHistory>
    {
        private readonly AppDbContext _db;

        public ItemPriceHistoryRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<ItemPriceHistory> CreateAsync(ItemPriceHistory data)
        {
            throw new NotImplementedException();
        }

        public Task<ItemPriceHistory> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ItemPriceHistory> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ItemPriceHistory>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ItemPriceHistory> UpdateAsync(ItemPriceHistory data)
        {
            throw new NotImplementedException();
        }
    }
}
