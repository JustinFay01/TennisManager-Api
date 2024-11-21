using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.data.Entities.Events;
using tennismanager.service.DTO.Event;
using tennismanager.service.DTO.Session;
using tennismanager.shared.Types;

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

        CreateMap<EventDto, Event>()
            .ReverseMap();
        
        CreateMap<RecurringPatternDto, RecurringPattern>()
            .ReverseMap();
        
        CreateMap<CondensedSessionDto, Session>()
                    .ReverseMap()
                    .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => src.Event.StartDate.ToDateTime(src.Event.StartTime ?? TimeOnly.MinValue).ToUniversalTime()))
                    .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => src.Event.StartDate.ToDateTime(src.Event.EndTime ?? TimeOnly.MinValue).ToUniversalTime()))
                    .ForMember(dest => dest.SessionDate, opt => opt.MapFrom(src => src.Event.StartDate));
        
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