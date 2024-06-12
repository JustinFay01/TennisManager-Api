using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.service.DTO;
using tennismanager.shared.Extensions;

namespace tennismanager.service.Profiles;

public class PackagePriceDtoProfile : Profile
{
    public PackagePriceDtoProfile()
    {
        CreateMap<PackagePriceDto, CoachPackagePrice>()
        .IgnoreAllUnmapped()
        .ForMember(dest => dest.PackageId, opt => opt.MapFrom(src => src.PackageId))
        .ForMember(dest => dest.PackageId, opt => opt.MapFrom(src => src.PackageId))
        .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));

    }
}