using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace TheLair.Extensions.Object;

public static partial class ObjectExtensions
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToJson(this object obj)
    {
        return (JsonConvert.SerializeObject(obj));
    }
}