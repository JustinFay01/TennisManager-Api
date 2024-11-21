namespace tennismanager.service.DTO.Session;

public class CondensedSessionDto
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public string? Description { get; set; }
    public int Capacity { get; set; }
    public Guid? CoachId { get; set; }
    public DateOnly SessionDate { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}