using TheLair.ASP_Net.OneOfLogic;

namespace TheLair.ASP_Net.Extensions;

public static class OneOfExtensions
{
    public static OneOf<T> OneOf<T>(this T obj)
    {
        return (new OneOf<T>(obj!));
    }
}