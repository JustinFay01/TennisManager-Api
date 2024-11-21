using tennismanager.service.DTO.Event;

namespace tennismanager.service.DTO.Session;

public class SessionDto
{
    public Guid? Id { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Capacity { get; set; }
    public required string Type { get; set; }
    public Guid? CoachId { get; set; }
    public required EventDto Event { get; set; }
}