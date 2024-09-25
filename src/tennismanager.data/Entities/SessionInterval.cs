using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities;

/// <summary>
///     https://stackoverflow.com/questions/5183630/calendar-recurring-repeating-events-best-storage-method
/// </summary>
public class SessionInterval : BaseEntity
{
    public SessionMeta SessionMeta { get; set; }
    public Guid SessionMetaId { get; set; }

    /// <summary>
    ///     The date and time the recurring event starts. Specifically, this is the first instance of this recurrence.
    ///     For instance, if the event was originally created on a Monday, but is also set to repeat on Mondays AND Tuesdays
    ///     every week,
    ///     this would be the date of the first Monday OR Tuesday and the RepeatInterval would be 604800 (seconds in a week).
    /// </summary>
    public DateTime RecurringStartDate { get; set; }

    /// <summary>
    ///     The number of seconds between each repeat (e.g. 86400 for daily, 604800 for weekly, 2592000 for monthly)
    /// </summary>
    public long RepeatInterval { get; set; }
}