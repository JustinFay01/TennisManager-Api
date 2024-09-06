namespace tennismanager.service.DTO.Session;

public class SessionIntervalDto
{
    public DateTime RecurringStartDate { get; set; }
    public TimeSpan RepeatInterval { get; set; }
}