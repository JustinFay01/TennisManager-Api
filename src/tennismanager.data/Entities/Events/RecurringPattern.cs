using tennismanager.data.Entities.Abstract;

namespace tennismanager.data.Entities.Events;

public enum RecurringType {
    Daily,
    Weekly,
    Monthly,
    Yearly
}

public class RecurringPattern : BaseEntity
{
    public required Event Event { get; set; }
    public Guid EventId { get; set; }
    
    /// <summary>
    /// Type of Recurring Pattern
    ///
    /// Could be Daily, Weekly, Monthly, Yearly
    /// </summary>
    public required RecurringType RecurringType { get; set; }

    /// <summary>
    /// Determines the frequency of the Recurring Pattern.
    /// For example, if the RecurringType is Weekly and the Frequency is 2, the event will occur every 2 weeks.
    ///
    /// Default value is <c>0</c>
    /// </summary>
    public int SeparationCount { get; set; }
    
    /// <summary>
    /// This is if we do not know the End Date, but we do know the number of occurrences.
    /// This is useful for events that have a set number of occurrences but no end date.
    /// </summary>
    public int? MaxOccurrences { get; set; }
    
    /// <summary>
    /// Used for a Weekly Occurrence. To specify which day of the week the event will occur.
    /// Also used for Monthly Occurrence to specify the day of the month.
    /// <code>
    /// "Bi-Weekly on Mondays"
    /// 
    /// RecurringType = Weekly
    /// SeparationCount = 1
    /// DayOfWeek = Monday
    /// </code>
    /// </summary>
    public DayOfWeek? DayOfWeek { get; set; }
    
    /// <summary>
    ///   Used for Monthly Occurrence to specify the week of the month.
    ///   <code>
    ///     "Monthly on the 2nd Monday"
    ///
    ///     RecurringType = Monthly
    ///     SeparationCount = 0
    ///     WeekOfMonth = 2
    ///     DayOfWeek = Monday
    ///    </code>
    ///
    ///     <code>
    ///     "Quarterly in the 11th day of the first month of the quarter"
    ///
    ///     RecurringType = Monthly
    ///     SeparationCount = 2
    ///     dayOfMonth = 11
    ///     All other fields are null
    ///     </code>
    /// </summary>
    public int? WeekOfMonth { get; set; }
    
    /// <summary>
    /// For Monthly Occurrence to specify the day of the month. See WeekOfMonth for more information.
    /// </summary>
    public int? DayOfMonth { get; set; }
    
    /// <summary>
    ///  Used for Yearly Occurrence to specify the month of the year.
    /// <code>
    /// "Yearly on the 3rd month"
    ///
    ///   RecurringType = Yearly
    ///   SeparationCount = 0
    ///   MonthOfYear = 3
    ///   All other fields are null
    /// </code>
    /// </summary>
    public int? MonthOfYear { get; set; }
}