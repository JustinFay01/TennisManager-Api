using tennismanager.service.DTO.Event;
using tennismanager.service.DTO.Session;
using tennismanager.shared.Types;

namespace CLI.Extensions;

public static class SessionDtoExtensions
{
    /// <summary>
    /// Generates a random SessionDto instance with default random values.
    /// </summary>
    public static SessionDto BuildRandom()
    {
        return new SessionDto
        {
            Name = $"Session_{Guid.NewGuid().ToString()[..8]}",
            Description = "A randomly generated session description.",
            Price = GetRandomPrice(),
            Capacity = GetRandomCapacity(),
            Type = GetRandomType(),
            CoachId = null,
            Event = new EventDto().BuildRandom()
        };
    }
    private static decimal GetRandomPrice()
    {
        var random = new Random();
        return Math.Round((decimal)random.NextDouble() * 100, 2); // Random price between 0 and 100
    }

    private static int GetRandomDuration()
    {
        var random = new Random();
        return random.Next(30, 120); // Random duration between 30 and 120 minutes
    }

    private static int GetRandomCapacity()
    {
        var random = new Random();
        return random.Next(5, 50); // Random capacity between 5 and 50
    }

    private static string GetRandomType()
    {
        var values = Enum.GetValues(typeof(SessionType));
        var random = new Random();
        var randomValue = (SessionType)values.GetValue(random.Next(values.Length));
        return randomValue.ToString(); // Convert the enum to a string
    }


}