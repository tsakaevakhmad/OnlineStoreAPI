using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Entities;
using System.Text.Json;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class CompanyRepository : IRepository<Company>
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CompanyRepository> _logger;
        private readonly IDistributedCache _cache;

        public CompanyRepository(AppDbContext db, ILogger<CompanyRepository> logger, IDistributedCache cache)
        {
            _db = db;
            _logger = logger;
            _cache = cache;
        }

        public async Task<Company> CreateAsync(Company data)
        {
            try
            {
                var result = await _db.Companies.AddAsync(data);
                await _db.SaveChangesAsync();

                List<Company> companies = new List<Company>();
                var companiesCashe = await _cache.GetStringAsync(nameof(companies));
                if (companiesCashe != null)
                {
                    companies = JsonSerializer.Deserialize<List<Company>>(companiesCashe);
                    companies.Add(result.Entity);
                    await _cache.SetStringAsync(nameof(companies), JsonSerializer.Serialize(companies), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });
                }

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
                await _cache.RemoveAsync(id.ToString());
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
                var companyCache = await _cache.GetStringAsync(id.ToString());
                if (companyCache != null)
                {
                    company = JsonSerializer.Deserialize<Company>(companyCache);
                }
                else
                {
                    company = await _db.Companies.FindAsync(id);
                    await _cache.SetStringAsync(id.ToString(), JsonSerializer.Serialize(company), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(2)
                    });
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
                IEnumerable<Company>? companies = new List<Company>();
                var companiesCashe = await _cache.GetStringAsync(nameof(companies));
                if (companiesCashe != null)
                {
                    companies = JsonSerializer.Deserialize<IEnumerable<Company>>(companiesCashe);
                }
                else
                {
                    companies = await _db.Companies.ToListAsync();
                    await _cache.SetStringAsync(nameof(companies), JsonSerializer.Serialize(companies), new DistributedCacheEntryOptions
                    {
                        AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
                    });
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
                await _cache.RemoveAsync(data.Id.ToString());
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
