namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemProperty
    {
        public long Id { get; set; }

        public string Name { get; set; }
        public string ValueType { get; set; } = "string";

        public List<ItemProperyValue>? ItemProperyValue { get; set; }

        public List<Category> ItemCategory { get; set; }
    }
}
