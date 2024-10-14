using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;

namespace OnlineStoreAPI.BLL.Interfaces
{
    public interface IItemServices
    {
        public Task<ResponseDTO<ItemDTO>> GetAsync(string id);
        public Task<ResponseDTO<IEnumerable<ItemShortDTO>>> GetAsync(int pageNumber = 1, int pageSize = 50);
        public Task<ResponseDTO<IEnumerable<ItemShortDTO>>> GetItemSearchArgumentsAsync(ItemSearchArguments searchArguments);
        public Task<ResponseDTO<ItemDTO>> UpdateAsync(ItemUpdateDTO data);
        public Task<ResponseDTO<ItemDTO>> DeleteAsync(string id);
        public Task<ResponseDTO<ItemDTO>> CreateAsync(ItemAddDTO data);
        public Task<ResponseDTO<ItemDTO>> CreateProperyAsync(ItemAddDTO data);
        public Task<ResponseDTO<IEnumerable<ItemPriceHistoryDTO>>> GetItemPriceHistoryAsync(string itemId);
        public Task<ResponseDTO<PropertyValuesDistinct>> GetDistinctValuesAsync(string itemCategoryId);
    }
}
