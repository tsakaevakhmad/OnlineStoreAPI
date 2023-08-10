using AutoMapper;
using LinqKit;
using Microsoft.Extensions.Primitives;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;
using System.Reflection;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineStoreAPI.BLL.Services
{
    public class ItemServices : IItemServices
    {
        private readonly IItemRepositories _repository;
        private readonly IMapper _mapper;

        public ItemServices(IMapper mapper, IItemRepositories repository) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<ItemDTO>> CreateAsync(ItemAddDTO data)
        {
            ItemDTO result = new ItemDTO();
            try
            {
                result = _mapper.Map<ItemDTO>(await _repository.CreateAsync(_mapper.Map<Item>(data)));
                return new ResponseDTO<ItemDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemDTO>(result) { Message = ex.Message };
            }
        }
        //?
        public async Task<ResponseDTO<ItemDTO>> CreateProperyAsync(ItemAddDTO data)
        {
            ItemDTO result = new ItemDTO();
            try
            {
                result = _mapper.Map<ItemDTO>(await _repository.CreateAsync(_mapper.Map<Item>(data)));
                return new ResponseDTO<ItemDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemDTO>> DeleteAsync(int id)
        {
            ItemDTO result = new ItemDTO();
            try
            {
                result = _mapper.Map<ItemDTO>(await _repository.DeleteAsync(id));
                return new ResponseDTO<ItemDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemDTO>> GetAsync(int id)
        {
            ItemDTO result = new ItemDTO();
            try
            {
                result = _mapper.Map<ItemDTO>(await _repository.GetAsync(id));
                return new ResponseDTO<ItemDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ItemShortDTO>>> GetAsync(string sortBy = null, string orderType = "DESC")
        {
            IEnumerable<ItemShortDTO> result = new List<ItemShortDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<ItemShortDTO>>(await _repository.GetAsync());             
                return new ResponseDTO<IEnumerable<ItemShortDTO>>(SortBy(ref result, sortBy, orderType));
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ItemShortDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ItemPriceHistoryDTO>>> GetItemPriceHistoryAsync(int itemId)
        {
            IEnumerable<ItemPriceHistoryDTO> result = new List<ItemPriceHistoryDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<ItemPriceHistoryDTO>>(await _repository.GetPriceHistoryAsync(itemId));
                return new ResponseDTO<IEnumerable<ItemPriceHistoryDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ItemPriceHistoryDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ItemShortDTO>>> GetItemSearchArgumentsAsync(ItemSearchArguments searchArguments, string sortBy = null, string orderType = "DESC")
        {
            IEnumerable<ItemShortDTO> result = new List<ItemShortDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<ItemShortDTO>>(await _repository.GetSearchArgumentsAsync(searchArguments));
                return new ResponseDTO<IEnumerable<ItemShortDTO>>(SortBy(ref result, sortBy, orderType));
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ItemShortDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemDTO>> UpdateAsync(ItemAddDTO data)
        {
            ItemDTO result = new ItemDTO();
            try
            {
                result = _mapper.Map<ItemDTO>(await _repository.UpdateAsync(_mapper.Map<Item>(data)));
                return new ResponseDTO<ItemDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<PropertyValuesDistinct>> GetDistinctValuesAsync(int itemCategoryId)
        {
            PropertyValuesDistinct result = new PropertyValuesDistinct();
            try
            {
                result = await _repository.GetPropertyValuesDistinct(itemCategoryId);
                return new ResponseDTO<PropertyValuesDistinct>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<PropertyValuesDistinct>(result) { Message = ex.Message };
            }
        }

        private IEnumerable<ItemShortDTO> SortBy(ref IEnumerable<ItemShortDTO> result, string sortBy, string orderType = "DESC")
        {
            sortBy = new StringBuilder(sortBy).Remove(0, 1).Insert(0, sortBy[0].ToString().ToUpper()).ToString();
            if (!string.IsNullOrEmpty(sortBy))
            {
                if(orderType.ToUpper() == "ASC")
                    return result.OrderBy(x => x.GetType().GetProperty(sortBy).GetValue(x));
                return result.OrderByDescending(x => x.GetType().GetProperty(sortBy).GetValue(x));
            }
            return result;
        }
    }
}
