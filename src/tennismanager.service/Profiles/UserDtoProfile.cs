using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Abstract;
using tennismanager.service.DTO;

namespace tennismanager.service.Profiles;

public class UserDtoProfile : Profile
{
    public UserDtoProfile()
    {
        CreateMap<UserDto, User>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .Include<CoachDto, Coach>()
            .Include<CustomerDto, Customer>()
            .ReverseMap();

        CreateMap<CoachDto, Coach>()
            .ForMember(dest => dest.PackagePricesList, opt => opt.MapFrom(src => src.PackagePricesList))
            .ReverseMap();
        
        CreateMap<CoachPackagePrice, PackagePriceDto>()
            .ForMember(dest => dest.PackageId, opt => opt.MapFrom(src => src.PackageId))
            .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price));
        
        CreateMap<CustomerDto, Customer>()
            .ReverseMap();
    }
}