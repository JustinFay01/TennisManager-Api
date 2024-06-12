using AutoMapper;
using tennismanager.api.Models.Package;
using tennismanager.service.DTO;

namespace tennismanager.api.Profiles;

public class PackageCreateProfile : Profile
{
    public PackageCreateProfile()
    {
        CreateMap<PackageCreateRequest, PackageDto>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DefaultPrice, opt => opt.MapFrom(src => src.DefaultPrice))
            .ForMember(dest => dest.Uses, opt => opt.MapFrom(src => src.Uses));


    }
    
}