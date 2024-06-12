using tennismanager_api.tennismanager.services.DTO.Abstract;

namespace tennismanager_api.tennismanager.services.DTO;

public class SessionDto : AuditableDto<Guid>
{
    public Guid Id { get; set; }
    public string Type { get; set; }
    public DateTime Date { get; set; }
    
    // TODO implement collections for DTOs
}