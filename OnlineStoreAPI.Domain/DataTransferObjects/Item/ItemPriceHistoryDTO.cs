namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemPriceHistoryDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Price { get; set; }
        public int ItemId { get; set; }
    }
}
