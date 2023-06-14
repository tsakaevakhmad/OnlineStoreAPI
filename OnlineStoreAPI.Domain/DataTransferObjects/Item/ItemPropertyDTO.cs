using OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory;

namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemPropertyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemCategoryDTO>? ItemCategories { get; set; }
    }
}
