using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;

namespace OnlineStoreAPI.BLL.Interfaces
{
    public interface IItemServices
    {
        public Task<ResponseDTO<ItemDTO>> GetAsync(int id);
        public Task<ResponseDTO<IEnumerable<ItemShortDTO>>> GetAsync();
        public Task<ResponseDTO<IEnumerable<ItemShortDTO>>> GetItemSearchArgumentsAsync(ItemSearchArguments searchArguments);
        public Task<ResponseDTO<ItemDTO>> UpdateAsync(ItemAddDTO data);
        public Task<ResponseDTO<ItemDTO>> DeleteAsync(int id);
        public Task<ResponseDTO<ItemDTO>> CreateAsync(ItemAddDTO data);
        public Task<ResponseDTO<ItemDTO>> CreateProperyAsync(ItemAddDTO data);
    }
}
