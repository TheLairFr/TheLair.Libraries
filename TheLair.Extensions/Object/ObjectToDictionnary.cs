using System.Reflection;

namespace TheLair.Extensions.Object;

public static partial class ObjectExtensions
{
    public static Dictionary<string, object?> ObjectToDictionnary(this object obj)
    {
        Dictionary<string, object?> dict = new();
        MemberInfo[] members = obj.GetType().GetMembers();

        foreach (MemberInfo member in members)
        {
            dict[member.Name] = member.GetType().Name.Replace("Runtime", "") switch
            {
                nameof(PropertyInfo) => member.As<PropertyInfo>().GetValue(obj),
                nameof(FieldInfo) => member.As<FieldInfo>().GetValue(obj),
                _ => null
            };
        }

        return (dict);
    }
}