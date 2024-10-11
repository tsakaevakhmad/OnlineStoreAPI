namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemUpdateDTO
    {
        public string? Id { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string CompanyId { get; set; }
        public string CategoryId { get; set; }
        public List<ItemPropertyValueUpdate>? ItemProperyValues { get; set; }
    }

    public class ItemPropertyValueUpdate
    {
        public string Value { get; set; }
        public string ItemPropertyId { get; set; }
    }
}
