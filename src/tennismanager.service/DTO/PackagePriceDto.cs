using tennismanager.service.DTO.Abstract;

namespace tennismanager.service.DTO;

public class PackagePriceDto : AuditableDto<Guid>
{
    public Guid CoachId { get; set; }
    public Guid PackageId { get; set; }
    public decimal Price { get; set; }
}