namespace OnlineStoreAPI.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<ItemCategory>? ItemCategories { get; set; }
    }
}