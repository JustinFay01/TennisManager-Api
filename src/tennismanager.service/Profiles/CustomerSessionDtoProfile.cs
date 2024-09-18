using AutoMapper;
using tennismanager.data.Entities;
using tennismanager.service.DTO.Session;

namespace tennismanager.service.Profiles;

public class CustomerSessionDtoProfile : Profile
{
    public CustomerSessionDtoProfile()
    {
        CreateMap<CustomerSessionDto, CustomerSession>()
            .ReverseMap();
    }
}