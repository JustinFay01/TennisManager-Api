﻿using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.service.DTO;
using tennismanager.service.DTO.Session;
using tennismanager.shared.Models;

namespace tennismanager.service.Profiles;

public class SessionDtoProfile : Profile
{
    public SessionDtoProfile()
    {
        CreateMap<SessionDto, Session>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => SessionTypeMapper.MapSessionType(src.Type)))
            .ReverseMap();

    }
}

public static class SessionTypeMapper
{
    public static SessionType MapSessionType(string type)
    {
        if (Enum.TryParse(type, true, out SessionType sessionType)) return sessionType;
        throw new ArgumentException($"Invalid session type: {type}");
    }
}