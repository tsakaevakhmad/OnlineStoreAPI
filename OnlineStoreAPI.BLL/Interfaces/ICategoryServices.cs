using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;

namespace OnlineStoreAPI.BLL.Interfaces
{
    public interface ICategoryServices
    {
        public Task<ResponseDTO<CategoryListDTO>> GetAsync(int id);
        public Task<ResponseDTO<IEnumerable<CategoryListDTO>>> GetAsync();
        public Task<ResponseDTO<CategoryListDTO>> UpdateAsync(CategoryListDTO data);
        public Task<ResponseDTO<CategoryListDTO>> DeleteAsync(int id);
        public Task<ResponseDTO<CategoryListDTO>> CreateAsync(CategoryListDTO data);
    }
}
