using AutoMapper;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;

        public CategoryServices(IMapper mapper, IRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        public async Task<ResponseDTO<CategoryShortDTO>> CreateAsync(CategoryShortDTO data)
        {
            CategoryShortDTO result = new CategoryShortDTO();
            try
            {
                result = _mapper.Map<CategoryShortDTO>(await _repository.CreateAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryShortDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryShortDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryShortDTO>> DeleteAsync(int id)
        {
            CategoryShortDTO result = new CategoryShortDTO();
            try
            {
                result = _mapper.Map<CategoryShortDTO>(await _repository.DeleteAsync(id));
                return new ResponseDTO<CategoryShortDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryShortDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryShortDTO>> GetAsync(int id)
        {
            CategoryShortDTO result = new CategoryShortDTO();
            try
            {
                result = _mapper.Map<CategoryShortDTO>(await _repository.GetAsync(id));
                return new ResponseDTO<CategoryShortDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryShortDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<CategoryShortDTO>>> GetAsync()
        {
            IEnumerable<CategoryShortDTO> result = new List<CategoryShortDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<CategoryShortDTO>>(await _repository.GetAsync());
                return new ResponseDTO<IEnumerable<CategoryShortDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<CategoryShortDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryShortDTO>> UpdateAsync(CategoryShortDTO data)
        {
            CategoryShortDTO result = new CategoryShortDTO();
            try
            {
                result = _mapper.Map<CategoryShortDTO>(await _repository.UpdateAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryShortDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryShortDTO>(result) { Message = ex.Message };
            }
        }
    }
}
