namespace tennismanager.shared.Extensions;

public static class EnumExtensions
{
    public static string ErrorMessage<TEnum>() where TEnum : Enum
    {
        return $"This property can only be {string.Join(" or ", Enum.GetNames(typeof(TEnum)))}";
    }
}