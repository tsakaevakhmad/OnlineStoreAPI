using AutoMapper;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.AutoMapper
{
    public class ItemProfile : Profile
    {
        public ItemProfile() 
        {
            CreateMap<Item, ItemAddDTO>()
                .ForMember(x => x.ItemProperyValues, x => x.MapFrom(x => x.ItemProperyValue))
                .ReverseMap()
                .ForMember(x => x.Id, e => e.Ignore());

            CreateMap<Item, ItemDTO>()
                .ForMember(x => x.ItemPropery, x=> x.MapFrom(x => x.ItemProperyValue))
                .ReverseMap()
                .ForMember(x => x.Id, e => e.Ignore());

            CreateMap<ItemProperyValue, ItemPropertyValueAdd>()
                .ReverseMap()
                .ForMember(x => x.Id, e => e.Ignore());

            CreateMap<Item, ItemShortDTO>()
                .ReverseMap();

            CreateMap<ItemPriceHistory, ItemPriceHistoryDTO>()
                .ReverseMap();

            CreateMap<ItemProperyValue, ItemPropertyList>()
                .ForMember(x => x.ItemPropertyValueId, x=> x.MapFrom(x => x.Id))
                .ForMember(x => x.Name, x=> x.MapFrom(x => x.ItemProperty.Name))
                .ReverseMap();
        }
    }
}
