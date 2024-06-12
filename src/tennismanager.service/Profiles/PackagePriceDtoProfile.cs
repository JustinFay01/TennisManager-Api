using AutoMapper;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager_api.tennismanager.services.Extensions;
using tennismanager_api.tennismanager.shared.Extensions;

namespace tennismanager_api.tennismanager.services.Profiles;

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