﻿using AutoMapper;
using tennismanager_api.tennismanager.api.Models.User;
using tennismanager_api.tennismanager.data.Entities;
using tennismanager_api.tennismanager.services.DTO;

namespace tennismanager_api.tennismanager.api.Profiles;

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

        CreateMap<UserCreateRequest, CoachDto>()
            .ForMember(dest => dest.PackagePricesList, opt => opt.MapFrom(src => src.PackagePrices));
        
        // TODO: Implement collections
        CreateMap<UserCreateRequest, CustomerDto>();
    }
}