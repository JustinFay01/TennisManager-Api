using AutoMapper;
using tennismanager.api.Models.Session;
using tennismanager.service.DTO.Session;

namespace tennismanager.api.Profiles;

public class SessionCreateProfile : Profile
{
    public SessionCreateProfile()
    {
        CreateMap<SessionRequest, SessionDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ReverseMap();

        CreateMap<SessionMetaRequest, SessionMetaDto>()
            .ReverseMap();

        CreateMap<SessionIntervalRequest, SessionIntervalDto>()
            .ReverseMap();
    }
}