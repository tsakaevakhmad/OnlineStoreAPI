namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemPriceHistory
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public DateTime DateTime { get; set; } = DateTime.UtcNow;
        
        public int ItemId { get; set; }
        public Item? Item { get; set; }
    }
}
