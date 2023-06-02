using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IItemRepositories : IRepository<Item>
    {
        public Task<ItemProperty> CreatePropertyAsync(ItemProperty data);
        public Task<ItemPriceHistory> GetPriceHistoryAsync(int itemId);
    }
}
