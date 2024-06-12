using AutoMapper;
using tennismanager.api.Models.User;
using tennismanager.service.DTO;

namespace tennismanager.api.Profiles;

public class UserCreateProfile : Profile
{
    public UserCreateProfile()
    {
        CreateMap<UserCreateRequest, UserDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .Include<UserCreateRequest, CoachDto>()
            .Include<UserCreateRequest, CustomerDto>();

        CreateMap<UserCreateRequest, CoachDto>();
        
        // TODO: Implement collections
        CreateMap<UserCreateRequest, CustomerDto>();
    }
}