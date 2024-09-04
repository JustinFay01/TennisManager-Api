using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

public class SessionMeta : BaseEntity
{
    public Session Session { get; set; }
    public int SessionId { get; set; }
    public bool Recurring { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public int CurrentWeekDay => (int)StartDate.DayOfWeek;
    public int CurrentWeekOfMonth => StartDate.Day / 7;
}