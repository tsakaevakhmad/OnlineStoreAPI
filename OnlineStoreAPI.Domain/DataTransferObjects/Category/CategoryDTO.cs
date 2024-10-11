namespace OnlineStoreAPI.Domain.DataTransferObjects.Category
{
    public class CategoryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ParentId { get; set; }
        public CategoryDTO Parent { get; set; }
        public List<CategoryDTO> Childrens { get; set; }
        public List<CategoryPropertyList>? ItemProperties { get; set; }
    }
}
