namespace StubGPT.Common;
public static class EnumExtensions
{
    #region Methods..
    public static TAttribute? GetCustomAttribute<TAttribute>(this Enum value) where TAttribute : Attribute
    {
        var enumType = value.GetType();
        var enumName = Enum.GetName(value.GetType(), value);
        return enumType.GetField(enumName)?.GetCustomAttributes(false).OfType<TAttribute>().SingleOrDefault();
    }
    #endregion Methods..
}
