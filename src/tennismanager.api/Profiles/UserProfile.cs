﻿using AutoMapper;
using tennismanager.api.Models.User.Abstract;
using tennismanager.api.Models.User.Requests;
using tennismanager.api.Models.User.Responses;
using tennismanager.service.DTO;

namespace tennismanager.api.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserCheckInRequest, UserDto>();
        
        CreateMap<UserCreateRequest, UserDto>()
            .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.GivenName))
            .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.FamilyName))
            .Include<UserCreateRequest, CoachDto>()
            .Include<UserCreateRequest, CustomerDto>();

        CreateMap<UserCreateRequest, CoachDto>()
            .IncludeBase<UserCreateRequest, UserDto>();

        // TODO: Implement collections
        CreateMap<UserCreateRequest, CustomerDto>();

        CreateMap<UserDto, UserResponse>()
            .Include<CoachDto, CoachResponse>();

        CreateMap<CoachDto, CoachResponse>();
    }
}