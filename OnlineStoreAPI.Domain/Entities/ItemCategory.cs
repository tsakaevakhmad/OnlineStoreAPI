namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemCategory
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public string CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Item>? Items { get; set; }

        public List<ItemProperty>? ItemProperty { get; set; } = new List<ItemProperty>();
    }
}