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

        public CompanyRepository(AppDbContext db, ILogger<CompanyRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task<Company> CreateAsync(Company data)
        {
            try
            {
                var result = await _db.Companies.AddAsync(data);
                await _db.SaveChangesAsync();
                return result.Entity;
            }
            catch(Exception ex)
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
                return await _db.Companies.FindAsync(id);
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
                return await _db.Companies.ToListAsync();
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
