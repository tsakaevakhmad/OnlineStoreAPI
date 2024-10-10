using OnlineStoreAPI.Domain.DataTransferObjects.Category;
using OnlineStoreAPI.Domain.DataTransferObjects.Company;

namespace OnlineStoreAPI.Domain.DataTransferObjects.Item
{
    public class ItemDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal? Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public CompanyShortDTO? Company { get; set; }
        public CategoryShortDTO? Category { get; set; }
        public List<ItemPropertyList> ItemPropery { get; set; }
    }
}
