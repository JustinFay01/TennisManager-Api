using AutoMapper;
using tennismanager.api.Models.Session;
using tennismanager.service.DTO;

namespace tennismanager.api.Profiles;

public class SessionCreateProfile : Profile
{
    public SessionCreateProfile()
    {
        CreateMap<SessionCreateRequest, SessionDto>();
    }
}