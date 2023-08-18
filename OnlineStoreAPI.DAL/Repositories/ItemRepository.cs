using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace OnlineStoreAPI.DAL.Repositories
{
    public class ItemRepository : IItemRepositories
    {
        private readonly AppDbContext _db;
        private readonly ILogger<ItemRepository> _logger;
        private readonly IRepositoryCacheServices _cacheServices;

        public ItemRepository(AppDbContext db, ILogger<ItemRepository> logger, IRepositoryCacheServices cacheServices) 
        { 
            _db = db; 
            _logger = logger;
            _cacheServices = cacheServices;
        }

        public async Task<Item> CreateAsync(Item data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var result = await _db.Items.AddAsync(data);
                await UpdatePriceHistoryAsync(data);
                await _db.ItemProperyValues.AddRangeAsync(data.ItemProperyValue);
                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.OnCreateAsync("items", result.Entity, 15);
                return result.Entity;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, $"Error when create item {data.Title}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        public async Task<ItemProperty> CreatePropertyAsync(ItemProperty data)
        {
            try
            {
                await _db.ItemPropertis.AddAsync(data);
                await _db.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when create item property {data.Name}");
                throw ex;
            }
        }

        public async Task<Item> DeleteAsync(int id)
        {
            try
            {
                var result = _db.Items.Remove(await _db.Items.FindAsync(id));
                await _db.SaveChangesAsync();
                await _cacheServices.OnDeleteAsync<Item>(id.ToString(), "items", 15, x => x.Id == id);
                return result.Entity;
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, $"Error when delete item {id}");
                throw ex;
            }
        }

        public async Task<Item> GetAsync(int id)
        {
            try
            {
                var item = await _cacheServices.OnGetAsync<Item>(id.ToString());
                if (item == null)
                {
                    item = await _db.Items
                    .Include(x => x.ItemPriceHistories)
                    .Include(x => x.ItemCategory)
                    .Include(x => x.Company)
                    .Include(x => x.ItemProperyValue)
                    .ThenInclude(x => x.ItemProperty)
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Id == id);
                    await _cacheServices.AddAsync(id.ToString(), item, 15);
                }
                return item;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get item by id {id}");
                throw ex;
            }
        }

        public async Task<IEnumerable<Item>> GetAsync()
        {
            try
            {
                var items = await _cacheServices.OnGetAsync<IEnumerable<Item>>("items");
                if (items == null)
                {
                    items = await _db.Items.ToListAsync();
                    await _cacheServices.AddAsync("items", items, 15);
                }
                return items;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get all items ");
                throw ex;
            }
        }

        public async Task<Item> UpdateAsync(Item data)
        {
            var transaction = await _db.Database.BeginTransactionAsync();
            try
            {
                var item = _db.Entry<Item>(data);
                item.State = EntityState.Modified;

                foreach(var itemProperyValues in data.ItemProperyValue)
                {
                    var values = _db.Entry<ItemProperyValue>(itemProperyValues);
                    values.State = EntityState.Modified;
                }

                await UpdatePriceHistoryAsync(data);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.OnUpdateAsync(data.Id.ToString(), "items", item.Entity, 15, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync(data.Id.ToString());
                return item.Entity;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when update item {data.Title}");
                await transaction.RollbackAsync();
                throw ex;
            }
        }

        private async Task UpdatePriceHistoryAsync(Item data)
        {
            try
            {
                await _db.ItemPriceHistories.AddAsync(new ItemPriceHistory { Item = data, Price = (decimal)data.Price });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<IEnumerable<Item>> GetSearchArgumentsAsync(ItemSearchArguments searchArguments)
        {
            try
            {
                var items = await _cacheServices.OnGetAsync<IEnumerable<Item>>("items");
                if (items == null)
                {
                    items = await _db.Items
                    .Include(x => x.ItemPriceHistories)
                    .Include(x => x.ItemCategory)
                    .Include(x => x.Company)
                    .Include(x => x.ItemProperyValue)
                    .ThenInclude(x => x.ItemProperty)
                    .AsNoTracking().ToListAsync();
                    await _cacheServices.AddAsync("items", items, 15);
                }
                return items.Where(GetItemExpression(searchArguments))
                    .Where(GetItemPropertyExpression(searchArguments.Property));               
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get item by search arguments {JsonSerializer.Serialize(searchArguments)}");
                throw ex;
            }
        }

        public async Task<IEnumerable<ItemPriceHistory>> GetPriceHistoryAsync(int itemId)
        {
            try
            {
                return await _db.ItemPriceHistories.Where(x => x.ItemId == itemId).ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get item price history itemId: {itemId}");
                throw ex;
            }
        }

        private ExpressionStarter<Item> GetItemExpression(ItemSearchArguments searchArguments)
        {
            var filter = PredicateBuilder.New<Item>(true);

            if (searchArguments.ItemCategoryId > 0)
            {
                filter = filter.And(item => item.ItemCategoryId == searchArguments.ItemCategoryId);
            }

            if (!string.IsNullOrEmpty(searchArguments.ItemName))
            {
                filter = filter.And(item => item.Title.ToUpper().Contains(searchArguments.ItemName.ToUpper()));
            }

            if (!string.IsNullOrEmpty(searchArguments.CompanyName))
            {
                filter = filter.And(item => item.Company.Name.ToUpper().Contains(searchArguments.CompanyName.ToUpper()));
            }

            if (searchArguments.FromPrice > 0 | searchArguments.ToPrice > 0)
            {
                filter = filter.And(item => item.Price >= searchArguments.FromPrice & item.Price <= searchArguments.ToPrice);
            }

            return filter;
        }

        private ExpressionStarter<Item> GetItemPropertyExpression(List<ItemPrpertySearchList> itemProperties)
        {
            var filter = PredicateBuilder.New<Item>(true);

            if (itemProperties != null && itemProperties.Count > 0)
            {
                filter = filter
                .And(item => item.ItemProperyValue
                .Any(prop => itemProperties
                .Any(x => (prop.ItemPropertyId == x.ItemPropertyId)
                    && (prop.ItemProperty.ValueType == "int" && Convert.ToInt32(prop.Value) >= Convert.ToInt32(x.ValueFrom) && Convert.ToInt32(prop.Value) <= Convert.ToInt32(x.ValueTo)
                        || prop.ItemProperty.ValueType == "bool" && Convert.ToBoolean(prop.Value) == Convert.ToBoolean(x.ValueFrom)
                        || prop.ItemProperty.ValueType == "double" && Convert.ToDouble(prop.Value) >= Convert.ToDouble(x.ValueFrom) && Convert.ToDouble(prop.Value) <= Convert.ToDouble(x.ValueTo)
                        || prop.Value.Contains(x.ValueFrom)))));
            }

            return filter;
        }

        public async Task<PropertyValuesDistinct> GetPropertyValuesDistinct(int itemCategoryId)
        {
            try
            {
                var result = await _db.ItemProperyValues
                    .AsNoTracking()
                    .Include(x => x.Item)
                    .ThenInclude(x => x.Company)
                    .Include(x => x.ItemProperty)
                    .Where(x => x.Item.ItemCategoryId == itemCategoryId)
                    .ToListAsync();

                return new PropertyValuesDistinct
                {
                    CompanyNames = result.Select(x => x.Item.Company.Name).Distinct().ToList(),
                    PropertyLists = (from props in result
                                     group props by props.ItemPropertyId into groupedProps
                                     select new PropertyValues
                                     {
                                         PropertyId = groupedProps.Key,
                                         PropertyName = groupedProps.First().ItemProperty.Name,
                                         Values = groupedProps.Select(x => x.Value).Distinct().ToList(),
                                     }).ToList()
                };
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when getting distinct values from Item Categoty id: {itemCategoryId}");
                throw ex;
            }
        }
    }
}
