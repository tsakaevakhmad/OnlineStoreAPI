namespace OnlineStoreAPI.Domain.Entities
{
    public class Item
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<ItemProperyValue> ItemProperyValue { get; set; }
        public List<ItemPriceHistory>? ItemPriceHistories { get; set; }
    }
}
