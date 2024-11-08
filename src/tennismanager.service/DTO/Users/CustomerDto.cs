using tennismanager.service.DTO.Session;

namespace tennismanager.service.DTO.Users;

public class CustomerDto : UserDto
{
    public ICollection<CustomerSessionDto> Sessions { get; set; } = [];
}