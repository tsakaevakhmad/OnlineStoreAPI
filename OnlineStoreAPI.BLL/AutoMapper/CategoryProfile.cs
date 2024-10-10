using AutoMapper;
using OnlineStoreAPI.Domain.DataTransferObjects.Category;
using OnlineStoreAPI.Domain.DataTransferObjects.Item;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.AutoMapper
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() 
        {
            CreateMap<Category, CategoryShortDTO>().ReverseMap();
            CreateMap<Category, CategoryDTO>()
                .ForMember(x => x.ItemProperties, e => e.MapFrom(x => x.ItemProperty))
                .ReverseMap();

            CreateMap<CategoryAdd, Category>()
               .ForMember(x => x.ItemProperty, e => e.MapFrom(x => x.ItemProperties))
               .ReverseMap();

            CreateMap<ItemPropertyAdd, ItemProperty>()
                .ReverseMap();

            CreateMap<ItemPropertyList, ItemProperty>()
                .ForMember(x => x.Id, e => e.MapFrom(x => x.ItemPropertyId))
                .ForMember(x => x.Name, e => e.MapFrom(x => x.Name))
                .ReverseMap();

            CreateMap<CategoryPropertyList, ItemProperty>()
                .ReverseMap();

            CreateMap<Category, CategoryAddProperties>()
                .ForMember(x => x.CategoryId, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.Properties, e => e.MapFrom(x => x.ItemProperty))
                .ReverseMap();

            CreateMap<Category, CategoryDeleteProperties>()
                .ForMember(x => x.CategoryId, e => e.MapFrom(x => x.Id))
                .ForMember(x => x.PropertyIds, e => e.MapFrom(x => x.ItemProperty))
                .ReverseMap();

            CreateMap<ItemProperty, int>()
                .ReverseMap()
                .ForMember(x => x.Id, e => e.MapFrom(x => x));

            CreateMap<UpdateCategory, Category>();

            CreateMap<Category, CategoryShortDTO>().ReverseMap();
        }
    }
}
