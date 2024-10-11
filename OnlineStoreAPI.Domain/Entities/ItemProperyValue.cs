namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemProperyValue
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Value { get; set; }

        public string ItemId { get; set; }
        public Item? Item { get; set; }

        public string ItemPropertyId { get; set; }
        public ItemProperty? ItemProperty { get; set; }
    }
}
