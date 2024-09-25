using AutoMapper;
using tennismanager.api.Models.Session;
using tennismanager.service.DTO.Session;

namespace tennismanager.api.Profiles;

public class CustomerSessionProfile : Profile
{
    public CustomerSessionProfile()
    {
        CreateMap<CustomerSessionRequest, CustomerSessionDto>()
            .ReverseMap();
    }
}