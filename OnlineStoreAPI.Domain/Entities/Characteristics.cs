namespace OnlineStoreAPI.Domain.Entities
{
    public class Characteristics
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public int CharacteristicValueId { get; set; }
        public CharacteristicValue CharacteristicValue { get; set; }

        public List<ItemCharacteristic> ItemCharacteristics { get; set; }
    }
}