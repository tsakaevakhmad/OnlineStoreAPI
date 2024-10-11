namespace OnlineStoreAPI.Domain.DataTransferObjects.Category
{
    public class CategoryAddProperties
    {
        public string CategoryId { get; set; }
        public List<CategoryPropertyList> Properties { get; set; }
    }
}
