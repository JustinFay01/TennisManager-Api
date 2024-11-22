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
                    .ForMember(dest => dest.StartTime, opt => opt.MapFrom(src => ToDateTime(src.Event.StartDate, src.Event.StartTime)))
                    .ForMember(dest => dest.EndTime, opt => opt.MapFrom(src => ToDateTime(src.Event.StartDate, src.Event.EndTime)))
                    .ForMember(dest => dest.SessionDate, opt => opt.MapFrom(src => src.Event.StartDate));
    }
    
    private DateTime? ToDateTime(DateOnly date, TimeOnly? time)
    {
        if( time == null ) return null;

        return date.ToDateTime((TimeOnly) time).ToUniversalTime();
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