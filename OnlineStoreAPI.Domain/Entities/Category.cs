namespace OnlineStoreAPI.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ParentId { get; set; }
        public Category Parent { get; set; }

        public List<Category> Childrens { get; set; }
        public List<Item>? Items { get; set; }
        public List<ItemProperty>? ItemProperty { get; set; } = new List<ItemProperty>();
    }
}