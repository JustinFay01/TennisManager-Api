namespace tennismanager.api.Models.Session;

public class SessionIntervalRequest
{
    public DateTime RecurringStartDate { get; set; }
    public long RepeatInterval { get; set; }
}