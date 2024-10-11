namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemPriceHistoryDTO
    {
        public string? Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public string ItemId { get; set; }
    }
}
