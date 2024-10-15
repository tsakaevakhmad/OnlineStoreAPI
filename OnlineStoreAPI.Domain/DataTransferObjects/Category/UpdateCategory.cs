namespace OnlineStoreAPI.Domain.DataTransferObjects.Category
{
    public class UpdateCategory
    {
        public string? Id { get; set; }
        public string Name { get; set; }
        public string? Icon { get; set;}
        public string? ParentId { get; set; }
    }
}
