using OnlineStoreAPI.Domain.DataTransferObjects.Item;

namespace OnlineStoreAPI.Domain.DataTransferObjects.Company
{
    public class CompanyDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ItemShortDTO>? Items { get; set; }
    }
}
