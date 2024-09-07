using AutoMapper;
using tennismanager.api.Models.Session;
using tennismanager.service.DTO;
using tennismanager.service.DTO.Session;

namespace tennismanager.api.Profiles;

public class SessionCreateProfile : Profile
{
    public SessionCreateProfile()
    {
        CreateMap<SessionCreateRequest, SessionDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore());

        CreateMap<SessionMetaRequest, SessionMetaDto>();
        
        CreateMap<SessionIntervalRequest, SessionIntervalDto>();
    }
}