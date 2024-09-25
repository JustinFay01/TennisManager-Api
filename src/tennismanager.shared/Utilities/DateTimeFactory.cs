namespace tennismanager.shared.Utilities;

public class DateTimeFactory
{
    public enum DateTimeType
    {
        Production,
        Test
    }

    public static DateTimeType CurrentDateTimeType { get; set; } = DateTimeType.Production;

    public static DateTime Now => CurrentDateTimeType switch
    {
        DateTimeType.Production => DateTime.Now,
        DateTimeType.Test => new DateTime(2022, 1, 1),
        _ => throw new ArgumentOutOfRangeException()
    };

    public static DateTime UtcNow => CurrentDateTimeType switch
    {
        DateTimeType.Production => DateTime.UtcNow,
        DateTimeType.Test => new DateTime(2022, 1, 1),
        _ => throw new ArgumentOutOfRangeException()
    };
}