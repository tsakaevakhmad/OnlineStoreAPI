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

        public async Task<ResponseDTO<CategoryListDTO>> CreateAsync(CategoryListDTO data)
        {
            CategoryListDTO result = new CategoryListDTO();
            try
            {
                result = _mapper.Map<CategoryListDTO>(await _repository.CreateAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryListDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryListDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryListDTO>> DeleteAsync(int id)
        {
            CategoryListDTO result = new CategoryListDTO();
            try
            {
                result = _mapper.Map<CategoryListDTO>(await _repository.DeleteAsync(id));
                return new ResponseDTO<CategoryListDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryListDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryListDTO>> GetAsync(int id)
        {
            CategoryListDTO result = new CategoryListDTO();
            try
            {
                result = _mapper.Map<CategoryListDTO>(await _repository.GetAsync(id));
                return new ResponseDTO<CategoryListDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryListDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<CategoryListDTO>>> GetAsync()
        {
            IEnumerable<CategoryListDTO> result = new List<CategoryListDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<CategoryListDTO>>(await _repository.GetAsync());
                return new ResponseDTO<IEnumerable<CategoryListDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<CategoryListDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryListDTO>> UpdateAsync(CategoryListDTO data)
        {
            CategoryListDTO result = new CategoryListDTO();
            try
            {
                result = _mapper.Map<CategoryListDTO>(await _repository.UpdateAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryListDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryListDTO>(result) { Message = ex.Message };
            }
        }
    }
}
