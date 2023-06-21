using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IItemRepositories : IRepository<Item>
    {
        public Task<ItemProperty> CreatePropertyAsync(ItemProperty data);
        public Task<IEnumerable<Item>> GetSearchArgumentsAsync(ItemSearchArguments searchArguments);
        public Task<IEnumerable<ItemPriceHistory>> GetPriceHistoryAsync(int itemId);
    }
}
