using AutoMapper;
using OnlineStoreAPI.Domain.DataTransferObjects.Company;
using OnlineStoreAPI.Domain.Entities;

namespace OnlineStoreAPI.BLL.AutoMapper
{
    public class CompanyProfile : Profile
    {
        public CompanyProfile() 
        {
            CreateMap<Company, CompanyDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, e => e.Ignore());

            CreateMap<Company, CompanyShortDTO>()
                .ReverseMap()
                .ForMember(x => x.Id, e => e.Ignore()); ;
        }
    }
}
