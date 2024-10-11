namespace OnlineStoreAPI.Domain.Entities
{
    public class Company
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }

        public List<Item>? Items { get; set;}

        public static implicit operator Company?(Category? v)
        {
            throw new NotImplementedException();
        }
    }
}