﻿using OnlineStoreAPI.Domain.DataTransferObjects.Company;
using OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory;

namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public CompanyListDTO? Company { get; set; }
        public ItemCategoryDTO? ItemCategory { get; set; }
        public List<ItemPropertyList> ItemPropery { get; set; }
    }
}
