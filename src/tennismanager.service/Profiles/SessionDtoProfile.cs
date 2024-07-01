using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.service.DTO;

namespace tennismanager.service.Profiles;

public class SessionDtoProfile : Profile
{
    public SessionDtoProfile()
    {
        CreateMap<SessionDto, Session>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => SessionTypeMapper.MapSessionType(src.Type)))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => DateTime.SpecifyKind(src.Date, DateTimeKind.Utc)))
            .ReverseMap();
    }
}

public static class SessionTypeMapper
{
    public static SessionType MapSessionType(string type)
    {
        return type switch
        {
            "event" => SessionType.Event,
            "tennisPrivate" => SessionType.TennisPrivate,
            "tennisDrill" => SessionType.TennisDrill,
            "tennisHitting" => SessionType.TennisHitting,
            "picklePrivate" => SessionType.PicklePrivate,
            "pickleDrill" => SessionType.PickleDrill,
            "pickleHitting" => SessionType.PickleHitting,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid session type.")
        };
    }
}