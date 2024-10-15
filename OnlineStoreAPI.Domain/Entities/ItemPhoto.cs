namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemPhoto
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Path { get; set; }

        public string ItemId { get; set; }
        public Item Item { get; set; } 
    }
}
