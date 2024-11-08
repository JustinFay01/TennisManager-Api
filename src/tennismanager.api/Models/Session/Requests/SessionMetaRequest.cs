namespace tennismanager.api.Models.Session.Requests;

public class SessionMetaRequest
{
    public bool? Recurring { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public List<SessionIntervalRequest>? SessionIntervals { get; set; } = [];
}