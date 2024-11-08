namespace tennismanager.api.Models.Session.Requests;

public class SessionIntervalRequest
{
    public DateTime? RecurringStartDate { get; set; }
    public long? RepeatInterval { get; set; }
}