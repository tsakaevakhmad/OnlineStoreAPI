namespace OnlineStoreAPI.Domain.DataTransferObjects.Category
{
    public class CategoryPropertyList
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Value { get; set; }
        public string? ValueType { get; set; }
    }
}
