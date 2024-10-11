using AutoMapper;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.BLL.Interfaces.Utilities;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.Services
{
    public class CategoryServices : ICategoryServices
    {
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _repository;
        private readonly ISortAndFilterManager _sortAndFilter;

        public CategoryServices(IMapper mapper, ICategoryRepository repository, ISortAndFilterManager sortAndFilter)
        {
            _mapper = mapper;
            _repository = repository;
            _sortAndFilter = sortAndFilter;
        }

        public async Task<ResponseDTO<CategoryDTO>> AddPropertyAsync(CategoryAddProperties data)
        {
            CategoryDTO result = new CategoryDTO();
            try
            {
                result = _mapper.Map<CategoryDTO>(await _repository.AddPropertyAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryDTO>> CreateAsync(CategoryAdd data)
        {
            CategoryDTO result = new CategoryDTO();
            try
            {
                result = _mapper.Map<CategoryDTO>(await _repository.CreateAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryDTO>> DeleteAsync(string id)
        {
            CategoryDTO result = new CategoryDTO();
            try
            {
                result = _mapper.Map<CategoryDTO>(await _repository.DeleteAsync(id));
                return new ResponseDTO<CategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryDTO>> DeletePropertyAsync(CategoryDeleteProperties data)
        {
            CategoryDTO result = new CategoryDTO();
            try
            {
                result = _mapper.Map<CategoryDTO>(await _repository.DeletePropertyAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryDTO>> GetAsync(string id)
        {
            CategoryDTO result = new CategoryDTO();
            try
            {
                result = _mapper.Map<CategoryDTO>(await _repository.GetAsync(id));
                return new ResponseDTO<CategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<CategoryDTO>>> GetAsync()
        {
            IEnumerable<CategoryDTO> result = new List<CategoryDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<CategoryDTO>>(await _repository.GetAsync());
                return new ResponseDTO<IEnumerable<CategoryDTO>>(_sortAndFilter.SortBy(result, "name", "asc"));
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<CategoryDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryDTO>> UpdateAsync(UpdateCategory data)
        {
            CategoryDTO result = new CategoryDTO();
            try
            {
                result = _mapper.Map<CategoryDTO>(await _repository.UpdateAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CategoryDTO>> UpdatePropertyAsync(CategoryAddProperties data)
        {
            CategoryDTO result = new CategoryDTO();
            try
            {
                result = _mapper.Map<CategoryDTO>(await _repository.UpdatePropertyAsync(_mapper.Map<Category>(data)));
                return new ResponseDTO<CategoryDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CategoryDTO>(result) { Message = ex.Message };
            }
        }
    }
}
