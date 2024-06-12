using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.service.DTO;

namespace tennismanager.service.Profiles;

public class PackageDtoProfile : Profile
{
    public PackageDtoProfile()
    {
        CreateMap<PackageDto, Package>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.DefaultPrice, opt => opt.MapFrom(src => src.DefaultPrice))
            .ForMember(dest => dest.Uses, opt => opt.MapFrom(src => src.Uses))
            .ReverseMap();
    }
}