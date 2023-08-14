using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CompanyRepository> _logger;
        private readonly IRepositoryCacheServices _cacheServices;

        public CompanyRepository(AppDbContext db, ILogger<CompanyRepository> logger, IRepositoryCacheServices cacheServices)
        {
            _db = db;
            _logger = logger;
            _cacheServices = cacheServices;
        }

        public async Task<Company> CreateAsync(Company data)
        {
            try
            {
                var result = await _db.Companies.AddAsync(data);
                await _db.SaveChangesAsync();
                await _cacheServices.OnCreateAsync<Company>("companies", result.Entity, 1);
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when try create company {data.Name}");
                throw ex;
            }
        }

        public async Task<Company> DeleteAsync(int id)
        {
            try
            {
                var result = _db.Companies.Remove(await _db.Companies.FindAsync(id));
                await _db.SaveChangesAsync();
                await _cacheServices.OnDeleteAsync<Company>(id.ToString(), "companies", 1, x => x.Id == id);
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when deletin company {id}");
                throw ex;
            }
        }

        public async Task<Company> GetAsync(int id)
        {
            try
            {
                Company company = new Company();
                company = await _cacheServices.OnGetAsync<Company>(id.ToString());
                if (company == null)
                {
                    company = await _db.Companies.FindAsync(id);
                    await _cacheServices.AddAsync(id.ToString(), company, 1);
                }
                return company;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get company by id: {id}");
                throw ex;
            }
        }

        public async Task<IEnumerable<Company>> GetAsync()
        {
            try
            {
                IEnumerable<Company>? companies = null;
                companies = await _cacheServices.OnGetAsync<List<Company>>(nameof(companies));
                if (companies == null)
                {
                    companies = await _db.Companies.ToListAsync();
                    await _cacheServices.AddAsync(nameof(companies), companies, 1);
                }

                return companies;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get all companies");
                throw ex;
            }
        }

        public async Task<Company> UpdateAsync(Company data)
        {
            try
            {
                var entity = _db.Entry<Company>(data);
                entity.State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await _cacheServices.OnUpdateAsync<Company>(data.Id.ToString(), "companies", entity.Entity, 1, x => x.Id == data.Id);
                return entity.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when update company {data.Id}");
                throw ex;
            }
        }
    }
}
