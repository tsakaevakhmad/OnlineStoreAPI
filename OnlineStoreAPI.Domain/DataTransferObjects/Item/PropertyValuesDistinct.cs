namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class PropertyValuesDistinct
    {
        public List<string> CompanyNames { get; set; }
        public List<PropertyValues> PropertyLists { get; set; }
    }

    public class PropertyValues
    {
        public int PropertyId { get; set; }
        public string PropertyName { get; set; }
        public List<string> Values { get; set; }
    }
}
