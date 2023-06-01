namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemProperty
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ItemProperyValue> ItemProperyValue { get; set; }
    }
}
