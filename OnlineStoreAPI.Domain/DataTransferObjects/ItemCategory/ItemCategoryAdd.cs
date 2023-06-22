using OnlineStoreAPI.Domain.DataTransferObjects.Item;

namespace OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory
{
    public class ItemCategoryAdd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryId { get; set; }
        public List<ItemPropertyAdd>? ItemProperties { get; set; }
    }
}
