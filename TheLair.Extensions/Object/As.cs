using System.Reflection;
using System.Runtime.CompilerServices;

namespace TheLair.Extensions.Object;

public static partial class ObjectExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static TType As<TType>(this object obj)
    {
        return ((TType) obj);
    }
}