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
        CreateMap<UserCreateRequest, UserDto>()
            .ConstructUsing(ReqToDto);
            
        
        CreateMap<UserUpdateRequest, UserDto>()
            .ConstructUsing(ReqToDto);
        CreateMap<UserCheckInRequest, UserDto>()
            .ConstructUsing(ReqToDto);
    }

    private static UserDto ReqToDto(UserRequest request, ResolutionContext context)
    {
        return request.Type switch
        {
            UserType.Coach => context.Mapper.Map<CoachDto>(request),
            UserType.Customer => context.Mapper.Map<CustomerDto>(request),
            _ => throw new ArgumentOutOfRangeException()
        };
    }
}

