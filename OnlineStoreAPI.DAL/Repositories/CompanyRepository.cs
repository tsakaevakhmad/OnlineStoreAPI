using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Constants;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class CompanyRepository : IRepository<Company, string>
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CompanyRepository> _logger;
        private readonly IRepositoryCacheServices _cacheServices;
        private readonly IFileStorage _fileStorage;

        public CompanyRepository(AppDbContext db, ILogger<CompanyRepository> logger, IRepositoryCacheServices cacheServices, IFileStorage fileStorage)
        {
            _db = db;
            _logger = logger;
            _cacheServices = cacheServices;
            _fileStorage = fileStorage;
        }

        public async Task<Company> CreateAsync(Company data)
        {
            try
            {
                var result = await _db.Companies.AddAsync(data);
                
                if(!string.IsNullOrEmpty(data.Logo))
                    data.Logo = await _fileStorage.AddAsync(data.Logo, 
                        data.Name + $"{Guid.NewGuid()}.png", string.Format(FileStoragePaths.CompanyPath, result.Entity.Id));

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

        public async Task<Company> DeleteAsync(string id)
        {
            try
            {
                var result = _db.Companies.Remove(await _db.Companies.FindAsync(id));
                await _fileStorage.DeleteAsync(result.Entity.Logo);
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

        public async Task<Company> GetAsync(string id)
        {
            try
            {
                var company = await _cacheServices.OnGetAsync<Company>(id.ToString());
                if (company == null)
                {
                    company = await _db.Companies.FindAsync(id);
                    company.Logo = await _fileStorage.GetUrlAsync(company.Logo);
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
                    var loadLogoTasks = companies.Select(async x => x.Logo = await _fileStorage.GetUrlAsync(x.Logo));
                    await Task.WhenAll(loadLogoTasks);
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

        public Task<IEnumerable<Company>> GetAsync(int pageNumber = 1, int pageSize = 50)
        {
            throw new NotImplementedException();
        }

        public async Task<Company> UpdateAsync(Company data)
        {
            try
            {
                if (!string.IsNullOrEmpty(data.Logo))
                    data.Logo = await _fileStorage.AddAsync(data.Logo,
                        data.Name.ToLower() + $"{Guid.NewGuid()}.png", string.Format(FileStoragePaths.CompanyPath, data.Id));

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
