using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IItemCategoryRepository : IRepository<ItemCategory>
    {
        public Task<ItemCategory> AddPropertyAsync(ItemCategory data);
        public Task<ItemCategory> DeletePropertyAsync(ItemCategory data);
    }
}
