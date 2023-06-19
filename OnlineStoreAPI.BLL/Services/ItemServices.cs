using AutoMapper;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;

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

        public async Task<ResponseDTO<IEnumerable<ItemShortDTO>>> GetAsync()
        {
            IEnumerable<ItemShortDTO> result = new List<ItemShortDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<ItemShortDTO>>(await _repository.GetAsync());
                return new ResponseDTO<IEnumerable<ItemShortDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<ItemShortDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<ItemShortDTO>>> GetItemSearchArgumentsAsync(ItemSearchArguments searchArguments)
        {
            IEnumerable<ItemShortDTO> result = new List<ItemShortDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<ItemShortDTO>>(await _repository.GetSearchArgumentsAsync(searchArguments));
                return new ResponseDTO<IEnumerable<ItemShortDTO>>(result);
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
    }
}
