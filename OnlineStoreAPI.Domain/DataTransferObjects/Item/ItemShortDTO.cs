namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemShortDTO
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}
