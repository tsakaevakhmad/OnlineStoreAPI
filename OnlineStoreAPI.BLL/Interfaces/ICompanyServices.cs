using OnlineStoreAPI.Domain.DataTransferObjects;
using OnlineStoreAPI.Domain.DataTransferObjects.Company;

namespace OnlineStoreAPI.BLL.Interfaces
{
    public interface ICompanyServices
    {
        public Task<ResponseDTO<CompanyDTO>> GetAsync(int id);
        public Task<ResponseDTO<IEnumerable<CompanyShortDTO>>> GetAsync();
        public Task<ResponseDTO<CompanyDTO>> UpdateAsync(CompanyShortDTO data);
        public Task<ResponseDTO<CompanyDTO>> DeleteAsync(int id);
        public Task<ResponseDTO<CompanyShortDTO>> CreateAsync(CompanyShortDTO data);
    }
}
