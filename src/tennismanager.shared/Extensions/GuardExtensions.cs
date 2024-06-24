using Dawn;
using tennismanager.shared.Utilities;

namespace tennismanager.shared.Extensions;

public static class GuardExtensions
{
    #region For DateTime

    public static ref readonly Guard.ArgumentInfo<T> IsValidDateTime<T>(this in Guard.ArgumentInfo<T> argument, string format,
        string? message = null)
    {
        if (argument.Value is DateTime dateTime)
        {
            if (dateTime == DateTime.MinValue || dateTime == DateTime.MaxValue)
            {
                throw new ArgumentException(message ?? $"{argument.Name} is not a valid date time.", argument.Name);
            }
        }
        else
        {
            var value = argument.Value as string;
            if (string.IsNullOrWhiteSpace(value) || !DateTime.TryParseExact(value, format, null, System.Globalization.DateTimeStyles.None, out _))
            {
                throw new ArgumentException(message ?? $"{argument.Name} is not a valid date time.", argument.Name);
            }
        }

        return ref argument;
    }
    
    public static ref readonly Guard.ArgumentInfo<T> DateTimeNotGreaterThanOrEqualTo<T>(this in Guard.ArgumentInfo<T> argument, DateTime notGreaterThanOrEqualTo,
        string? message = null)
    {
        if(argument.Value is DateTime dateTime)
        {
            if (dateTime >= notGreaterThanOrEqualTo)
            {
                throw new ArgumentException(message ?? $"{argument.Name} must be less than {DateTimeFactory.UtcNow}.", argument.Name);
            }
        }
        else
        {
            throw new ArgumentException(message ?? $"{argument.Name} is not a valid date time.", argument.Name);
        }
        
        return ref argument;
    }
    # endregion
}