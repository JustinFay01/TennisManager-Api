using AutoMapper;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager_api.tennismanager.services.Extensions;

namespace tennismanager_api.tennismanager.services.Profiles;

public class PackageDtoProfile : Profile
{
    public PackageDtoProfile()
    {
        CreateMap<PackageDto, Package>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DefaultPrice, opt => opt.MapFrom(src => src.DefaultPrice))
            .ForMember(dest => dest.Uses, opt => opt.MapFrom(src => src.Uses));
    }
}