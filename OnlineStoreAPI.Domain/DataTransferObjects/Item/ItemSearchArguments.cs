namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemSearchArguments
    {
        public int? ItemCategoryId { get; set; }
        public Decimal? FromPrice { get; set; }
        public Decimal? ToPrice { get; set; }
        public string? ItemName { get; set; }
        public string? CompanyName { get; set; }
        public List<ItemPropertyList>? Property { get; set; }
    }
}
