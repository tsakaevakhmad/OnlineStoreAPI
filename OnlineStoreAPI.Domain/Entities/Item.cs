﻿namespace OnlineStoreAPI.Domain.Entities
{
    public class Item
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime ReleaseDate { get; set; }

        public int CompanyId { get; set; }
        public Company? Company { get; set; }

        public int ItemCategoryId { get; set; }
        public ItemCategory ItemCategory { get; set; }

        public List<ItemCharacteristic> ItemCharacteristic { get; set; }
        public List<ItemPriceHistory> ItemPriceHistories { get; set; }
    }
}
