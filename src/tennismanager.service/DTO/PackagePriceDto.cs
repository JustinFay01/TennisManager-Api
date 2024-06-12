using tennismanager_api.tennismanager.services.DTO.Abstract;

namespace tennismanager_api.tennismanager.services.DTO;

public class PackagePriceDto : AuditableDto<Guid>
{
    public Guid CoachId { get; set; }
    public Guid PackageId { get; set; }
    public decimal Price { get; set; }
}