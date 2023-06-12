using AutoMapper;
using OnlineStoreAPI.BLL.Interfaces;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Company;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.Services
{
    public class CompanyServices : ICompanyServices
    {
        private readonly IRepository<Company> _repository;
        private readonly IMapper _mapper;

        public CompanyServices(IRepository<Company> repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ResponseDTO<CompanyShortDTO>> CreateAsync(CompanyShortDTO data)
        {
            CompanyShortDTO result = new CompanyShortDTO();
            try
            {
                result = _mapper.Map<CompanyShortDTO>(await _repository.CreateAsync(_mapper.Map<Company>(data)));
                return new ResponseDTO<CompanyShortDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CompanyShortDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CompanyDTO>> DeleteAsync(int id)
        {
            CompanyDTO result = new CompanyDTO();
            try
            {
                result = _mapper.Map<CompanyDTO>(await _repository.DeleteAsync(id));
                return new ResponseDTO<CompanyDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CompanyDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CompanyDTO>> GetAsync(int id)
        {
            CompanyDTO result = new CompanyDTO();
            try
            {
                result = _mapper.Map<CompanyDTO>(await _repository.GetAsync(id));
                return new ResponseDTO<CompanyDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CompanyDTO>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<IEnumerable<CompanyShortDTO>>> GetAsync()
        {
            IEnumerable<CompanyShortDTO> result = new List<CompanyShortDTO>();
            try
            {
                result = _mapper.Map<IEnumerable<CompanyShortDTO>>(await _repository.GetAsync());
                return new ResponseDTO<IEnumerable<CompanyShortDTO>>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<IEnumerable<CompanyShortDTO>>(result) { Message = ex.Message };
            }
        }

        public async Task<ResponseDTO<CompanyDTO>> UpdateAsync(CompanyShortDTO data)
        {
            CompanyDTO result = new CompanyDTO();
            try
            {
                result = _mapper.Map<CompanyDTO>(await _repository.UpdateAsync(_mapper.Map<Company>(data)));
                return new ResponseDTO<CompanyDTO>(result);
            }
            catch (Exception ex)
            {
                return new ResponseDTO<CompanyDTO>(result) { Message = ex.Message };
            }
        }
    }
}
