using OnlineStoreAPI.Domain.DataTransferObjects.Category;

namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemPropertyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoryDTO>? ItemCategories { get; set; }
    }
}
