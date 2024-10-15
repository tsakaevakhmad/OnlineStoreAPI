
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.Constants;
using OnlineStoreAPI.Domain.Entities;
using System.Security.Cryptography.X509Certificates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _db;
        private readonly ILogger<CategoryRepository> _logger;
        private readonly IRepositoryCacheServices _cacheServices;
        private readonly IFileStorage _fileStorage;

        public CategoryRepository(AppDbContext db, ILogger<CategoryRepository> logger, IRepositoryCacheServices cacheServices, IFileStorage fileStorage)
        {
            _db = db;
            _logger = logger;
            _cacheServices = cacheServices;
            _fileStorage = fileStorage;
        }

        public async Task<Category> CreateAsync(Category data)
        {
            try
            {
                await AddFileAsync(data);
                data = (await _db.Categories.AddAsync(data)).Entity;
                await _db.SaveChangesAsync();
                await _cacheServices.OnCreateAsync("categories", data, 1);
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when try create item with name: \"{data.Name}\" in category repository");
                throw ex;
            }
        }

        public async Task<Category> DeleteAsync(string id)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var requst = await _db.Categories
                    .AsNoTracking()
                    .Include(x => x.ItemProperty)
                    .FirstOrDefaultAsync(x => x.Id == id);

                var result = _db.Categories
                    .Remove(requst);
                _db.ItemProperties.RemoveRange(requst.ItemProperty);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _fileStorage.DeleteAsync(result.Entity.Icon);

                await _cacheServices.OnDeleteAsync<Category>(id.ToString(), "categories", 1, x => x.Id == id);
                await _cacheServices.DeleteAsync("items");
                return result.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete Category {id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<Category> GetAsync(string id)
        {
            try
            {
                Category category;
                category = await _cacheServices.OnGetAsync<Category>(id.ToString());
                if (category == null)
                {
                    category = await _db.Categories
                        .Include(x => x.Childrens)
                        .Include(x => x.ItemProperty)
                        .FirstAsync(x => x.Id == id);
                    await LoadChildrenAsync(category);
                    await _cacheServices.AddAsync(id.ToString(), category, 1);                    
                    await LoadChildrenIconAsync(category);
                }
                return category;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when try get by id with id: \"{id}\" in category repository");
                throw ex;
            }
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            try
            {
                List<Category> categories;
                categories = await _cacheServices.OnGetAsync<List<Category>>(nameof(categories));
                if (categories == null)
                {
                    categories = await _db.Categories.Where(x => x.ParentId == null).Include(x => x.Childrens).ToListAsync();
                    foreach (var category in categories)
                    {
                        await LoadChildrenAsync(category);
                        await LoadChildrenIconAsync(category);
                    }
                    await _cacheServices.AddAsync(nameof(categories), categories, 1);
                }
                return categories.OrderBy(x => x.Name);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error when try get all in category repository");
                throw ex;
            }
        }

        public async Task<Category> UpdateAsync(Category data)
        {
            try
            {
                await AddFileAsync(data);
                var entity = await _db.Categories.FirstOrDefaultAsync(x => x.Id == data.Id);

                if (!string.IsNullOrEmpty(data.Icon))
                    entity.Icon = data.Icon;
                entity.ParentId = data.ParentId;
                entity.Name = data.Name;    
                await _db.SaveChangesAsync();
                await _cacheServices.OnUpdateAsync<Category>(data.Id.ToString(), "categories", entity, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync(data.Id.ToString());
                await _cacheServices.DeleteAsync("items");
                return entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when update Category {data.Id}");
                throw ex;
            }
        }

        public async Task<Category> AddPropertyAsync(Category data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.Categories
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == data.Id);
                result.ItemProperty.AddRange(data.ItemProperty);
                _db.Categories.Update(result);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.DeleteAsync(data.Id.ToString());
                await _cacheServices.DeleteAsync("items");
                await _cacheServices.DeleteAsync("categories");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when add Category to item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<Category> DeletePropertyAsync(Category data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.Categories
                .Include(x => x.ItemProperty)
                .FirstOrDefaultAsync(x => x.Id == data.Id);

                result.ItemProperty.RemoveAll(x => data.ItemProperty.Any(e => e.Id == x.Id));

                _db.Entry(result).State = EntityState.Modified;
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.OnUpdateAsync<Category>(data.Id.ToString(), "categories", result, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync("items");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete Category from item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<Category> UpdatePropertyAsync(Category data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.Categories
                .Include(x => x.ItemProperty)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == data.Id);
                //Нужно пофиксить
                foreach (var property in data.ItemProperty)
                {
                    int index = result.ItemProperty.IndexOf(result.ItemProperty.FirstOrDefault(x => x.Id == property.Id));
                    result.ItemProperty[index] = property;
                    var itemPropertyEntity = _db.Entry<ItemProperty>(result.ItemProperty[index]);
                    itemPropertyEntity.State = EntityState.Modified;
                }

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();

                await _cacheServices.OnUpdateAsync<Category>(data.Id.ToString(), "categories", result, 1, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync("items");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete Category to item {data.Id}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public Task<IEnumerable<Category>> GetAsync(int pageNumber = 1, int pageSize = 50)
        {
            throw new NotImplementedException();
        }

        private async Task AddFileAsync(Category category)
        {
            if (category == null)
                return;

            if (category.Icon != null)
                category.Icon = await _fileStorage.AddAsync(category.Icon, category.Name + $"_{Guid.NewGuid()}.png", string.Format(FileStoragePaths.CategoryPath, category.Name));
            
            if (category.Childrens.Any())
                foreach (var child in category.Childrens)
                    await AddFileAsync(child);
        }

        private async Task LoadChildrenAsync(Category category)
        {
            if (category != null && category.Childrens.Any())
            {
                foreach (var child in category.Childrens)
                {
                    await _db.Entry(child).Collection(l => l.Childrens).LoadAsync();
                    await LoadChildrenAsync(child);
                }
            }
        }

        private async Task LoadChildrenIconAsync(Category category)
        {

            if (category == null)
                return;

            if (category.Icon != null)
                category.Icon = await _fileStorage.GetUrlAsync(category.Icon);

            if (category.Childrens.Any())
                foreach (var child in category.Childrens)
                    await LoadChildrenIconAsync(child);
        }
    }
}
