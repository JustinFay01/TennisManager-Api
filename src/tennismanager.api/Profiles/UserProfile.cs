using AutoMapper;
using tennismanager.api.Models.User.Abstract;
using tennismanager.api.Models.User.Requests;
using tennismanager.api.Models.User.Responses;
using tennismanager.service.DTO;

namespace tennismanager.api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCreateRequest, UserDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .Include<UserCreateRequest, CoachDto>()
            .Include<UserCreateRequest, CustomerDto>();

        CreateMap<UserCreateRequest, CoachDto>()
            .IncludeBase<UserCreateRequest, UserDto>();

        // TODO: Implement collections
        CreateMap<UserCreateRequest, CustomerDto>();

        CreateMap<UserDto, UserResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.PhoneNumber))
            .Include<CoachDto, CoachResponse>();

        CreateMap<CoachDto, CoachResponse>();
    }
}