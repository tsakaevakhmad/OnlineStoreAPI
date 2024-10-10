namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemPriceHistory
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        
        public long ItemId { get; set; }
        public Item? Item { get; set; }
    }
}
