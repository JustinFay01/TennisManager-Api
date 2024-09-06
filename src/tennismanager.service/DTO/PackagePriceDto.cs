namespace tennismanager.service.DTO;

public class PackagePriceDto
{
    public Guid CoachId { get; set; }
    public Guid PackageId { get; set; }
    public decimal Price { get; set; }
}