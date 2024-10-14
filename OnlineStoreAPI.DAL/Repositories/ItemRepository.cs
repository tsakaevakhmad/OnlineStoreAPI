using LinqKit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OnlineStoreAPI.DAL.Contexts;
using OnlineStoreAPI.DAL.Interfaces;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;
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

                var categoryValues = await _db.Categories.AsNoTracking().Include(x => x.ItemProperty).FirstOrDefaultAsync(x => x.Id == result.Entity.CategoryId);
                foreach(var itemProperty in data.ItemProperyValue)
                    if (!categoryValues.ItemProperty.Any(x => x.Id == itemProperty.ItemPropertyId))
                        throw new Exception($"Category of this item has no item property: \"{itemProperty.ItemPropertyId}\"");

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
                await _db.ItemProperties.AddAsync(data);
                await _db.SaveChangesAsync();
                return data;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when create item property {data.Name}");
                throw ex;
            }
        }

        public async Task<Item> DeleteAsync(string id)
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

        public async Task<Item> GetAsync(string id)
        {
            try
            {
                var item = await _cacheServices.OnGetAsync<Item>(id.ToString());
                if (item == null)
                {
                    item = await _db.Items
                    .Include(x => x.ItemPriceHistories)
                    .Include(x => x.Category)
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

        public async Task<IEnumerable<Item>> GetAsync(int pageNumber = 1, int pageSize = 50)
        {
            try
            {
                var items = await _cacheServices.OnGetAsync<IEnumerable<Item>>("items");
                if (items == null)
                {
                    items = await _db.Items
                        .Include(x => x.ItemPriceHistories)
                        .Include(x => x.Category)
                        .Include(x => x.Company)
                        .Include(x => x.ItemProperyValue)
                        .ThenInclude(x => x.ItemProperty)
                        .AsNoTracking()
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToListAsync();
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
                var item = await _db.Items
                    .Include(x => x.ItemProperyValue)
                    .FirstOrDefaultAsync(x => x.Id == data.Id);
                var categoryValues = await _db.Categories.AsNoTracking().Include(x => x.ItemProperty).FirstOrDefaultAsync(x => x.Id == item.CategoryId);

                foreach (var itemProperyValues in data.ItemProperyValue)
                {
                    if(!categoryValues.ItemProperty.Any(x => x.Id == itemProperyValues.ItemPropertyId))
                        continue;

                    var value = item.ItemProperyValue.FirstOrDefault(x => x.ItemPropertyId == itemProperyValues.ItemPropertyId);
                    if (value == null)
                        item.ItemProperyValue.Add(value);
                    else
                        value.Value = itemProperyValues.Value;
                }

                await UpdatePriceHistoryAsync(data);

                await _db.SaveChangesAsync();
                await transaction.CommitAsync();
                await _cacheServices.OnUpdateAsync(data.Id.ToString(), "items", item, 15, x => x.Id == data.Id);
                await _cacheServices.DeleteAsync(data.Id.ToString());
                return item;
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
                data.ItemPriceHistories = new List<ItemPriceHistory> { new ItemPriceHistory { Item = data, Price = (decimal)data.Price } };
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
                var items = await _db.Items
                    .Include(x => x.ItemPriceHistories)
                    .Include(x => x.Category)
                    .Include(x => x.Company)
                    .Include(x => x.ItemProperyValue)
                    .ThenInclude(x => x.ItemProperty)
                    .AsNoTracking()
                    .Where(GetItemExpression(searchArguments))
                    .Where(GetItemPropertyExpression(searchArguments.Property))
                    .OrderBy(x => x.ReleaseDate)
                    .Skip((searchArguments.PageNumber - 1) * searchArguments.PageSize)
                        .Take(searchArguments.PageSize)
                    .ToListAsync();

                return items;
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, $"Error when get item by search arguments {JsonSerializer.Serialize(searchArguments)}");
                throw ex;
            }
        }

        public async Task<IEnumerable<ItemPriceHistory>> GetPriceHistoryAsync(string itemId)
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

            if (!string.IsNullOrEmpty(searchArguments.ItemCategoryId))
                filter = filter.And(item => item.CategoryId == searchArguments.ItemCategoryId);

            if (!string.IsNullOrEmpty(searchArguments.ItemName))
                filter = filter.And(item => item.Title.ToUpper().Contains(searchArguments.ItemName.ToUpper()));

            if (!string.IsNullOrEmpty(searchArguments.CompanyName))
                filter = filter.And(item => item.Company.Name.ToUpper().Contains(searchArguments.CompanyName.ToUpper()));

            if (searchArguments.FromPrice > 0 | searchArguments.ToPrice > 0)
                filter = filter.And(item => item.Price >= searchArguments.FromPrice & item.Price <= searchArguments.ToPrice);

            return filter;
        }

        private ExpressionStarter<Item> GetItemPropertyExpression(List<ItemPropertySearchList> itemProperties)
        {
            var filter = PredicateBuilder.New<Item>(true);

            if (itemProperties == null || itemProperties.Count == 0)
                return filter;

            foreach (var property in itemProperties)
            {
                if (int.TryParse(property.ValueFrom, out _))
                {
                    filter = filter.And(item => item.ItemProperyValue
                        .Any(prop => prop.ItemPropertyId == property.ItemPropertyId
                            && Convert.ToInt32(prop.Value) >= Convert.ToInt32(property.ValueFrom)
                            && Convert.ToInt32(prop.Value) <= Convert.ToInt32(property.ValueTo)));
                }
                else if (bool.TryParse(property.ValueFrom, out _))
                {
                    filter = filter.And(item => item.ItemProperyValue
                        .Any(prop => prop.ItemPropertyId == property.ItemPropertyId
                            && Convert.ToBoolean(prop.Value) == Convert.ToBoolean(property.ValueFrom)));
                }
                else if (double.TryParse(property.ValueFrom, out _))
                {
                    filter = filter.And(item => item.ItemProperyValue
                        .Any(prop => prop.ItemPropertyId == property.ItemPropertyId
                            && Convert.ToDouble(prop.Value) >= Convert.ToDouble(property.ValueFrom)
                            && Convert.ToDouble(prop.Value) <= Convert.ToDouble(property.ValueTo)));
                }
                else
                {
                    filter = filter.And(item => item.ItemProperyValue
                        .Any(prop => prop.ItemPropertyId == property.ItemPropertyId
                            && prop.Value.ToUpper().Contains(property.ValueFrom.ToUpper())));
                }
            }

            return filter;
        }

        /* private ExpressionStarter<Item> GetItemPropertyExpression(List<ItemPropertySearchList> itemProperties)
         {
             var filter = PredicateBuilder.New<Item>(true);

             if (itemProperties != null && itemProperties.Count > 0)
             {
                 filter = filter
                 .And(item => item.ItemProperyValue
                 .Any(prop => itemProperties
                 .Any(x => (prop.ItemPropertyId == x.ItemPropertyId)
                     && (prop.ItemProperty.ValueType == "int" ? Convert.ToInt32(prop.Value) >= Convert.ToInt32(x.ValueFrom) && Convert.ToInt32(prop.Value) <= Convert.ToInt32(x.ValueTo)
                     : prop.ItemProperty.ValueType == "bool" ? Convert.ToBoolean(prop.Value) == Convert.ToBoolean(x.ValueFrom)
                     : prop.ItemProperty.ValueType == "double" ? Convert.ToDouble(prop.Value) >= Convert.ToDouble(x.ValueFrom) && Convert.ToDouble(prop.Value) <= Convert.ToDouble(x.ValueTo)
                     : prop.Value.Contains(x.ValueFrom)))));
             }

             return filter;
         }*/

        public async Task<PropertyValuesDistinct> GetPropertyValuesDistinct(string itemCategoryId)
        {
            try
            {
                var result = await _db.ItemProperyValues
                    .AsNoTracking()
                    .Include(x => x.Item)
                    .ThenInclude(x => x.Company)
                    .Include(x => x.ItemProperty)
                    .Where(x => x.Item.CategoryId == itemCategoryId)
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

        public Task<IEnumerable<Item>> GetAsync()
        {
            throw new NotImplementedException();
        }
    }
}
