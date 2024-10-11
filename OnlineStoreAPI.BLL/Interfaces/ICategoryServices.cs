using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;

namespace OnlineStoreAPI.BLL.Interfaces
{
    public interface ICategoryServices
    {
        public Task<ResponseDTO<CategoryDTO>> GetAsync(string id);
        public Task<ResponseDTO<IEnumerable<CategoryDTO>>> GetAsync();
        public Task<ResponseDTO<CategoryDTO>> UpdateAsync(UpdateCategory data);
        public Task<ResponseDTO<CategoryDTO>> DeleteAsync(string id);
        public Task<ResponseDTO<CategoryDTO>> CreateAsync(CategoryAdd data);
        public Task<ResponseDTO<CategoryDTO>> DeletePropertyAsync(CategoryDeleteProperties data);
        public Task<ResponseDTO<CategoryDTO>> AddPropertyAsync(CategoryAddProperties data);
        public Task<ResponseDTO<CategoryDTO>> UpdatePropertyAsync(CategoryAddProperties data);
    }
}
