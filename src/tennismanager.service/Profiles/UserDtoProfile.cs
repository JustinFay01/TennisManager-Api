using AutoMapper;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.data.Entities.Abstract;
using tennismanager_api.tennismanager.services.DTO;

namespace tennismanager_api.tennismanager.services.Profiles;

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
        
        CreateMap<CustomerDto, Customer>()
            .ReverseMap();
    }
}