using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly AppDbContext _db;

        public CompanyRepository(AppDbContext db)
        {
            _db = db;
        }

        public Task<Company> CreateAsync(Company data)
        {
            throw new NotImplementedException();
        }

        public Task<Company> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Company> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Company>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Company> UpdateAsync(Company data)
        {
            throw new NotImplementedException();
        }
    }
}
