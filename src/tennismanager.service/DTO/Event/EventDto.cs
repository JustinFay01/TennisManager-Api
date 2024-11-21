namespace tennismanager.service.DTO.Event;

public class EventDto
{
    public Guid Id { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly? EndDate { get; set; }
    public TimeOnly? StartTime { get; set; }
    public TimeOnly? EndTime { get; set; }
    public bool IsFullDay { get; set; }
    public bool IsRecurring { get; set; }
    public Guid? ParentEventId { get; set; }
    public List<RecurringPatternDto> RecurringPatterns { get; set; } = [];
}