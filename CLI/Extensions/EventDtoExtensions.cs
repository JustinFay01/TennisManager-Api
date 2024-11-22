using CLI.Util;
using tennismanager.data.Entities.Events;
using tennismanager.service.DTO.Event;

namespace CLI.Extensions;

public static class EventDtoExtensions
{
    /// <summary>
    /// Generates a random EventDto instance with realistic random values.
    /// </summary>
    public static EventDto BuildRandom(this EventDto eventDto)
    {
        var random = new Random();

        // Generate random start and end dates within a range
        var startDate = DateOnly.FromDateTime(DateTime.UtcNow.AddDays(random.Next(1, 30)));
        DateOnly? endDate = random.Next(0, 2) == 0 // 50% chance of having an end date
            ? startDate.AddDays(random.Next(1, 7)) // End date within a week
            : null;

        var startTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(random.Next(0, 24)));
        var endTime = TimeOnly.FromDateTime(DateTime.UtcNow.AddHours(random.Next(1, 24))); // End time within a day
        var isRecurring = random.Next(0, 2) == 1; // 50% chance of being recurring
        
        return new EventDto
        {
            StartDate = startDate,
            EndDate = endDate,
            StartTime = startTime,
            EndTime = endTime,
            IsFullDay = false,
            IsRecurring = isRecurring, // 50% chance of being recurring
            ParentEventId = random.Next(0, 2) == 1 ? Guid.NewGuid() : null, // 50% chance of having a parent event
            RecurringPatterns = isRecurring ? GenerateRecurringPatterns(random.Next(1, 4), Guid.Empty) : []
        };
    }
    
    private static List<RecurringPatternDto> GenerateRecurringPatterns(int count, Guid eventId)
    {
        var random = new Random();
        var patterns = new List<RecurringPatternDto>();
    
        for (var i = 0; i < count; i++)
        {
            var recurringType =  EnumExtensions.GetRandomEnumValue<RecurringType>();
            patterns.Add(new RecurringPatternDto
            {
                EventId = eventId,
                SeparationCount = random.Next(1, 5), // Random separation between occurrences (e.g., every 1-5 days/weeks/etc.)
                MaxOccurrences = random.Next(0, 2) == 1 ? random.Next(1, 11) : null, // 50% chance of having a max occurrence
                RecurringType = recurringType.ToString(),
                DayOfWeek = recurringType == RecurringType.Weekly ? (DayOfWeek?)random.Next(0, 7) : null, // Only set for Weekly
                WeekOfMonth = recurringType == RecurringType.Monthly ? random.Next(1, 5) : null, // Only set for Monthly
                MonthOfYear = recurringType == RecurringType.Yearly ? random.Next(1, 13) : null // Only set for Yearly
            });
        }
    
        return patterns;
    }
}