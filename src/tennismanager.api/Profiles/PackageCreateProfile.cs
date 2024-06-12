using AutoMapper;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager.api.Models.Package;

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