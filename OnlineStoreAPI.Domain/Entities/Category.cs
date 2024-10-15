namespace OnlineStoreAPI.Domain.Entities
{
    public class Category
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public string? Icon { get; set; }
        public string? ParentId { get; set; }
        public Category? Parent { get; set; }

        public List<Category> Childrens { get; set; }
        public List<Item>? Items { get; set; }
        public List<ItemProperty>? ItemProperty { get; set; } = new List<ItemProperty>();

        public static implicit operator Task<object>(Category v)
        {
            throw new NotImplementedException();
        }
    }
}