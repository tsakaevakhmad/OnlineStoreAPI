namespace OnlineStoreAPI.Domain.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Item>? Items { get; set;}

        public static implicit operator Company?(Category? v)
        {
            throw new NotImplementedException();
        }
    }
}