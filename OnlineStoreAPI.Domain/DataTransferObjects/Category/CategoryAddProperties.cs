namespace OnlineStoreAPI.Domain.DataTransferObjects.Category
{
    public class CategoryAddProperties
    {
        public int CategoryId { get; set; }
        public List<CategoryPropertyList> Properties { get; set; }
    }
}
