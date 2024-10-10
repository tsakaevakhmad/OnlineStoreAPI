using AutoMapper;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.AutoMapper
{
    public class ItemCategoryProfile : Profile
    {
        public ItemCategoryProfile() 
        {
            CreateMap<ItemPropertyAdd, ItemProperty>()
                .ReverseMap();

            CreateMap<ItemPropertyList, ItemProperty>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.ItemPropertyId))
                .ForMember(x => x.Name, e => e.MapFrom(x => x.Name))
                .ReverseMap();

            CreateMap<ItemProperty, long>()
                .ReverseMap()
                .ForMember(x => x.Id, e => e.MapFrom(x => x));
        }
    }
}
