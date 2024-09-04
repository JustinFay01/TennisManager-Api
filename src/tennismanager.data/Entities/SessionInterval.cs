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
    ///     The Unix timestamp of the start of the repeat interval
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    ///     The number of seconds between each repeat (e.g. 86400 for daily, 604800 for weekly, 2592000 for monthly)
    /// </summary>
    public int RepeatInterval { get; set; }
}