namespace CLI.Util;

public static class EnumExtensions
{
    /// <summary>
    /// Gets a random value from any enum and returns it as a string.
    /// </summary>
    /// <typeparam name="T">The enum type.</typeparam>
    /// <returns>A random value from the enum as a string.</returns>
    public static T GetRandomEnumValue<T>() where T : Enum
    {
        var values = Enum.GetValues(typeof(T));
        var random = new Random();
        var randomValue = (T)values.GetValue(random.Next(values.Length))!;
        return randomValue;
    }
}