namespace tennismanager_api.tennismanager.services.DTO;

public class CoachDto : UserDto
{ 
    public ICollection<PackagePriceDto> PackagePricesList { get; set; } = new List<PackagePriceDto>();
}