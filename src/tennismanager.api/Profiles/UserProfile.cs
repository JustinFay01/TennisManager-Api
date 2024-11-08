using AutoMapper;
using tennismanager.api.Models.User.Abstract;
using tennismanager.api.Models.User.Requests;
using tennismanager.api.Models.User.Responses;
using tennismanager.service.DTO;
using tennismanager.shared.Types;

namespace tennismanager.api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserDto, UserResponse>()
            .ReverseMap();
        
        CreateMap<CustomerDto, UserResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => UserType.Customer))
            .IncludeBase<UserDto, UserResponse>();
        
        CreateMap<CoachDto, UserResponse>()
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => UserType.Coach))
            .IncludeBase<UserDto, UserResponse>();

        CreateMap<UserRequest, CustomerDto>();
        
        CreateMap<UserRequest, CoachDto>();
        
        CreateMap<UserUpdateRequest, UserDto>()
            .ReverseMap();

    }
}

