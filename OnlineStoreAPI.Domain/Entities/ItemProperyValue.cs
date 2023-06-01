namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemProperyValue
    {
        public int Id { get; set; }
        public string Value { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int ItemPropertyId { get; set; }
        public ItemProperty ItemProperty { get; set; }
    }
}
