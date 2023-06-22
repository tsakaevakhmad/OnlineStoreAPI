using AutoMapper;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.Services
{
    public class ItemCategoryServices : IItemCategoryServices
    {
        private readonly IMapper _mapper;
        private readonly IItemCategoryRepository _repository;

        public ItemCategoryServices(IMapper mapper, IItemCategoryRepository repository) 
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseDTO<ItemCategoryDTO>> AddPropertyAsync(ItemCategoryAddProperties data)
        {
            ItemCategoryDTO result = new ItemCategoryDTO();
            try
            {
                result = _mapper.Map<ItemCategoryDTO>(await _repository.AddPropertyAsync(_mapper.Map<ItemCategory>(data)));
                return new ResponseDTO<ItemCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemCategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemCategoryDTO>> CreateAsync(ItemCategoryAdd data)
        {
            ItemCategoryDTO result = new ItemCategoryDTO();
            try
            {
                result = _mapper.Map<ItemCategoryDTO>(await _repository.CreateAsync(_mapper.Map<ItemCategory>(data)));
                return new ResponseDTO<ItemCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemCategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemCategoryDTO>> DeleteAsync(int id)
        {
            ItemCategoryDTO result = new ItemCategoryDTO();
            try
            {
                result = _mapper.Map<ItemCategoryDTO>(await _repository.DeleteAsync(id));
                return new ResponseDTO<ItemCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemCategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemCategoryDTO>> DeletePropertyAsync(ItemCategoryDeleteProperties data)
        {
            ItemCategoryDTO result = new ItemCategoryDTO();
            try
            {
                result = _mapper.Map<ItemCategoryDTO>(await _repository.DeletePropertyAsync(_mapper.Map<ItemCategory>(data)));
                return new ResponseDTO<ItemCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemCategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemCategoryDTO>> GetAsync(int id)
        {
            ItemCategoryDTO result = new ItemCategoryDTO();
            try
            {
                result = _mapper.Map<ItemCategoryDTO>(await _repository.GetAsync(id));
                return new ResponseDTO<ItemCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemCategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ItemCategoryDTO>>> GetAsync()
        {
            IEnumerable<ItemCategoryDTO> result = new List<ItemCategoryDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<ItemCategoryDTO>>(await _repository.GetAsync());
                return new ResponseDTO<IEnumerable<ItemCategoryDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ItemCategoryDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemCategoryDTO>> UpdateAsync(UpdateItemCategory data)
        {
            ItemCategoryDTO result = new ItemCategoryDTO();
            try
            {
                result = _mapper.Map<ItemCategoryDTO>(await _repository.UpdateAsync(_mapper.Map<ItemCategory>(data)));
                return new ResponseDTO<ItemCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemCategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<ItemCategoryDTO>> UpdatePropertyAsync(ItemCategoryAddProperties data)
        {
            ItemCategoryDTO result = new ItemCategoryDTO();
            try
            {
                result = _mapper.Map<ItemCategoryDTO>(await _repository.UpdatePropertyAsync(_mapper.Map<ItemCategory>(data)));
                return new ResponseDTO<ItemCategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<ItemCategoryDTO>(result) { Message = ex.Message };
            }
        }
    }
}
