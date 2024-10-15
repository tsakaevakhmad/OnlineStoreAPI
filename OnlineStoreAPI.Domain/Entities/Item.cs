namespace OnlineStoreAPI.Domain.Entities
{
    public class Item
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public string CompanyId { get; set; }
        public Company? Company { get; set; }

        public string CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<ItemPhoto> ItemPhotos { get; set; }
        public List<ItemProperyValue> ItemProperyValue { get; set; }
        public List<ItemPriceHistory>? ItemPriceHistories { get; set; }
    }
}
