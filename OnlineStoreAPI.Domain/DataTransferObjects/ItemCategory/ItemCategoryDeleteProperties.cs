namespace OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory
{
    public class ItemCategoryDeleteProperties
    {
        public int ItemCategoryId { get; set; }
        public List<int> PropertyIds { get; set; }
    }
}
