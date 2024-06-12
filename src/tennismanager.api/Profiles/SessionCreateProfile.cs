using AutoMapper;
using tennismanager_api.tennismanager.api.Models.Session;
using tennismanager_api.tennismanager.services.DTO;

namespace tennismanager_api.tennismanager.api.Profiles;

public class SessionCreateProfile : Profile
{
    public SessionCreateProfile()
    {
        CreateMap<SessionCreateRequest, SessionDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date));
    }
}