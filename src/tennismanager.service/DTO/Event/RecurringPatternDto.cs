namespace tennismanager.service.DTO.Event;

public class RecurringPatternDto
{
    public Guid Id { get; set; }
    public Guid EventId { get; set; }
    public int SeparationCount { get; set; }
    public int? MaxOccurrences { get; set; }
    public required string RecurringType { get; set; }
    public DayOfWeek? DayOfWeek { get; set; }
    public int? WeekOfMonth { get; set; }
    public int? MonthOfYear { get; set; }
}