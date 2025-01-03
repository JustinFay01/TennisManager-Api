using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities.Events;

/// <summary>
/// <see href="https://vertabelo.com/blog/again-and-again-managing-recurring-events-in-a-data-model/">Managing Recurring Events in a Data Model</see>
/// </summary>
public class Event : BaseEntity
{
    /// <summary>
    /// Initial Start Date of the Event
    /// </summary>
    public DateOnly StartDate { get; set; }
    
    
    /// <summary>
    /// Optional End Date of the Event
    /// </summary>
    public DateOnly? EndDate { get; set; }
    
    /// <summary>
    /// Start Time of the Event
    /// Nullable if IsFullDay is true
    /// </summary>
    public TimeOnly? StartTime { get; set; }
    
    /// <summary>
    /// End Time of the Event
    /// Nullable if IsFullDay is true
    /// </summary>
    public TimeOnly? EndTime { get; set; }
    
    public bool IsFullDay { get; set; }
    public bool IsRecurring { get; set; }
    
    public Event? ParentEvent { get; set; }
    public Guid? ParentEventId { get; set; }
    
    // Navigation Properties
    
    /// <summary>
    /// List of Recurring Patterns for this Event
    /// </summary>
    public ICollection<RecurringPattern> RecurringPatterns { get; set; } = [];

    // Auditable Fields later
}