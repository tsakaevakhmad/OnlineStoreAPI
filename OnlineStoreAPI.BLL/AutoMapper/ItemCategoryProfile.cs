using AutoMapper;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.DataTransferObjects.ItemCategory;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.AutoMapper
{
    public class ItemCategoryProfile : Profile
    {
        public ItemCategoryProfile() 
        {
            CreateMap<ItemCategoryDTO, ItemCategory>()
                .ForMember(x => x.ItemProperty, e => e.MapFrom(x => x.ItemProperties))
                .ReverseMap()
                .ForMember(x => x.CategoryName, e => e.MapFrom(w => w.Category.Name));
            
            CreateMap<ItemPropertyList, ItemProperty>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.ItemPropertyId))
                .ForMember(x => x.Name, e => e.MapFrom(x => x.Name))
                .ReverseMap();

            CreateMap<ItemCategoryPropertyList, ItemProperty>()
                .ReverseMap();

            CreateMap<ItemCategory, ItemCategoryAddProperties>()
                .ForMember(x => x.ItemCategoryId, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Properties, e => e.MapFrom(x => x.ItemProperty))
                .ReverseMap();

            CreateMap<ItemCategory, ItemCategoryDeleteProperties>()
                .ForMember(x => x.ItemCategoryId, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.PropertyIds, e => e.MapFrom(x => x.ItemProperty))
                .ReverseMap();

            CreateMap<ItemProperty, int>()
                .ReverseMap()
                .ForMember(x => x.Id, e => e.MapFrom(x => x));
                
        }
    }
}
