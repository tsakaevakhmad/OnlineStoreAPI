using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;

namespace OnlineStoreAPI.BLL.Interfaces
{
    public interface ICategoryServices
    {
        public Task<ResponseDTO<CategoryShortDTO>> GetAsync(int id);
        public Task<ResponseDTO<IEnumerable<CategoryShortDTO>>> GetAsync();
        public Task<ResponseDTO<CategoryShortDTO>> UpdateAsync(CategoryShortDTO data);
        public Task<ResponseDTO<CategoryShortDTO>> DeleteAsync(int id);
        public Task<ResponseDTO<CategoryShortDTO>> CreateAsync(CategoryShortDTO data);
    }
}
