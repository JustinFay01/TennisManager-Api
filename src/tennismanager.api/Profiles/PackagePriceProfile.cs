using AutoMapper;
using tennismanager_api.tennismanager.services.DTO;
using tennismanager.api.Models.User;

namespace tennismanager.api.Profiles;

public class PackagePriceProfile : Profile
{
  public PackagePriceProfile()
  {
    CreateMap<PackagePriceRequest, PackagePriceDto>()
      .ForMember(dest => dest.CoachId, opt => opt.MapFrom(src => src.CoachId))
      .ForMember(dest => dest.PackageId, opt => opt.MapFrom(src => src.PackageId))
      .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
  }   
}