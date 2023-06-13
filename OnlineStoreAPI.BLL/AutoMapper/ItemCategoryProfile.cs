using AutoMapper;
using OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.AutoMapper
{
    public class ItemCategoryProfile : Profile
    {
        public ItemCategoryProfile() 
        {
            CreateMap<ItemCategoryDTO, ItemCategory>().ReverseMap()
                .ForMember(x => x.CategoryName, e => e.MapFrom(w => w.Category.Name));
        }
    }
}
