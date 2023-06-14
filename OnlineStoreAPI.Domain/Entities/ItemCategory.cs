namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemCategory
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Item>? Items { get; set; }

        public List<ItemProperty>? ItemProperty { get; set; } = new List<ItemProperty>();
    }
}