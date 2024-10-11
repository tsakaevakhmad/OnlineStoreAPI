namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemProperty
    {
        public string Id { get; set; } = Guid.NewGuid().ToString(); 

        public string Name { get; set; }
        public string ValueType { get; set; } = "string";

        public List<ItemProperyValue>? ItemProperyValue { get; set; }

        public List<Category> Category { get; set; }
    }
}
