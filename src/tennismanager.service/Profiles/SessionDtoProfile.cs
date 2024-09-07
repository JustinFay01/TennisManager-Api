using AutoMapper;
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
            .ForMember(dest => dest.Coach, opt => opt.Ignore())
            .ForMember(dest => dest.CustomerSessions, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => SessionTypeMapper.MapSessionType(src.Type)))
            .ReverseMap();

        CreateMap<SessionMetaDto, SessionMeta>()
            .ForMember(dest => dest.Session, opt => opt.Ignore())
            .ForMember(dest => dest.SessionId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<SessionIntervalDto, SessionInterval>()
            .ForMember(dest => dest.SessionMeta, opt => opt.Ignore())
            .ForMember(dest => dest.SessionMetaId, opt => opt.Ignore())
            .ForMember(dest => dest.Id, opt => opt.Ignore())
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