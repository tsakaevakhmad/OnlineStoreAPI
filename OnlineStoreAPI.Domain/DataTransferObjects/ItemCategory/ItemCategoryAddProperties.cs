namespace OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory
{
    public class ItemCategoryAddProperties
    {
        public int ItemCategoryId { get; set; }
        public List<ItemCategoryPropertyList> Properties { get; set; }
    }
}
