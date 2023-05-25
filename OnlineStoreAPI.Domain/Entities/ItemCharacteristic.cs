namespace OnlineStoreAPI.Domain.Entities
{
    public class ItemCharacteristic
    {
        public int Id { get; set; }

        public int ItemId { get; set; }
        public Item Item { get; set; }

        public int CharacteristicsId { get; set; }
        public Characteristics Characteristics { get; set; }
    }
}
