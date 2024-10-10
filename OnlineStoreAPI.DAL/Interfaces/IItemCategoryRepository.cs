using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Interfaces
{
    public interface IItemCategoryRepository : IRepository<Category>
    {
        public Task<Category> AddPropertyAsync(Category data);
        public Task<Category> DeletePropertyAsync(Category data);
        public Task<Category> UpdatePropertyAsync(Category data);
    }
}
