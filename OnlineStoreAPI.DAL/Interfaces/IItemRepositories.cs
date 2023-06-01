using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface ItemRepositories : IRepository<Item>
    {
        public Task<ItemProperty> CreateProperty(ItemProperty data);
    }
}
