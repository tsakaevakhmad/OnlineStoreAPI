namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemSearchArguments
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string? ItemCategoryId { get; set; }
        public Decimal? FromPrice { get; set; }
        public Decimal? ToPrice { get; set; }
        public string? ItemName { get; set; } = null;
        public string? CompanyName { get; set; } = null;
        public List<ItemPropertySearchList>? Property { get; set; } = new List<ItemPropertySearchList>();
    }
}
