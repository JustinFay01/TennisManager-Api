using AutoMapper;
using tennismanager.api.Models.Session;
using tennismanager.service.DTO;

namespace tennismanager.api.Profiles;

public class SessionCreateProfile : Profile
{
    public SessionCreateProfile()
    {
        CreateMap<SessionCreateRequest, SessionDto>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type))
            .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date))
            .ForMember(dest => dest.CoachId,
                opt => opt.MapFrom(src => string.IsNullOrEmpty(src.CoachId) ? Guid.Empty : Guid.Parse(src.CoachId)));
    }
}