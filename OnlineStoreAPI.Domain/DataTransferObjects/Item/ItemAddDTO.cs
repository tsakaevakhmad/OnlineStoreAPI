namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemAddDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public int CompanyId { get; set; }
        public int ItemCategoryId { get; set; }
        public List<ItemPropertyValueAdd>? ItemProperyValues { get; set; }
    }

    public class ItemPropertyValueAdd
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int ItemPropertyId { get; set; }
    } 
}
