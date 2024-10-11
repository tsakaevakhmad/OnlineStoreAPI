using AutoMapper;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.AutoMapper
{
    public class ItemCategoryProfile : Profile
    {
        public ItemCategoryProfile() 
        {
            CreateMap<ItemProperty, ItemPropertyAdd>()
                .ReverseMap()
                .ForMember(x => x.Id, e => e.Ignore());

            CreateMap<ItemPropertyList, ItemProperty>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.ItemPropertyId))
                .ForMember(x => x.Name, e => e.MapFrom(x => x.Name))
                .ReverseMap();

            CreateMap<ItemProperty, string>()
                .ReverseMap()
                .ForMember(x => x.Id, e => e.MapFrom(x => x));
        }
    }
}
