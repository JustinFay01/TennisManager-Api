using AutoMapper;
using tennismanager.api.Models.User.Abstract;
using tennismanager.api.Models.User.Requests;
using tennismanager.api.Models.User.Responses;
using tennismanager.service.DTO.Users;
using tennismanager.shared.Types;

namespace tennismanager.api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserResponse, UserDto>()
            .Include<CoachResponse, CoachDto>()
            .Include<CustomerResponse, CustomerDto>()
            .ReverseMap()
            .ForMember(dest => dest.Type, opt => opt.MapFrom<UserTypeResolver>());

        CreateMap<CoachResponse, CoachDto>()
            .ReverseMap();

        CreateMap<CustomerResponse, CustomerDto>()
            .ReverseMap();

        CreateMap<UserRequest, CustomerDto>();
        CreateMap<UserRequest, CoachDto>();
    }
    
    private class UserTypeResolver : IValueResolver<UserDto, UserResponse, string>
    {
        public string Resolve(UserDto source, UserResponse destination, string destMember, ResolutionContext context)
        {
            return source switch
            {
                CoachDto => UserType.Coach.ToString().ToLower(),
                CustomerDto => UserType.Customer.ToString().ToLower(),
                _ => throw new ArgumentException("Invalid user type")
            };
        }
    }
}

