namespace tennismanager.api.Models.Session.Responses;

public class SessionResponse
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
    public required string Type { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Capacity { get; set; }
    public Guid? CoachId { get; set; }
    public DateOnly SessionDate { get; set; }
    public DateTime? StartTime { get; set; }
    public DateTime? EndTime { get; set; }
}