using AutoMapper;
using tennismanager_api.tennismanager.api.Models.Package;
using tennismanager_api.tennismanager.services.DTO;

namespace tennismanager_api.tennismanager.api.Profiles;

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