using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Company;

namespace OnlineStoreAPI.BLL.Interfaces
{
    public interface ICompanyServices
    {
        public Task<ResponseDTO<CompanyDTO>> GetAsync(int id);
        public Task<ResponseDTO<IEnumerable<CompanyListDTO>>> GetAsync();
        public Task<ResponseDTO<CompanyDTO>> UpdateAsync(CompanyDTO data);
        public Task<ResponseDTO<CompanyDTO>> DeleteAsync(int id);
        public Task<ResponseDTO<CompanyDTO>> CreateAsync(CompanyDTO data);
    }
}
