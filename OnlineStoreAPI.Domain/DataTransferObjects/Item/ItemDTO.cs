using OnlineStoreAPI.Domain.DataTransferObjects.Company;
using OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory;

namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public CompanyShortDTO? Company { get; set; }
        public ItemCategoryShort? ItemCategory { get; set; }
        public List<ItemPropertyList> ItemPropery { get; set; }
    }
}
