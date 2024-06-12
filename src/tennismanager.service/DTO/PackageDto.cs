using tennismanager.service.DTO.Abstract;

namespace tennismanager.service.DTO;

public class PackageDto : AuditableDto<Guid>
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int Uses { get; set; }
    public decimal DefaultPrice { get; set; }
    
    //TODO: Add collections
}