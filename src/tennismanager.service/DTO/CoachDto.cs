namespace tennismanager.service.DTO;

public class CoachDto : UserDto
{ 
    public ICollection<PackagePriceDto> PackagePricesList { get; set; } = new List<PackagePriceDto>();
}