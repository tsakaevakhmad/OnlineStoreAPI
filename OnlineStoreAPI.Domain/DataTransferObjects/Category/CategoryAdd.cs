using OnlineStoreAPI.Domain.DataTransferObjects.Item;

namespace OnlineStoreAPI.Domain.DataTransferObjects.Category
{
    public class CategoryAdd
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParrentId { get; set; }
        public List<CategoryAdd>? Childrens { get; set; }
        public List<ItemPropertyAdd>? ItemProperties { get; set; }
    }
}
