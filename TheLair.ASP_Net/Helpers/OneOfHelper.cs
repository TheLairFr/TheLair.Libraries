using Microsoft.Extensions.DependencyInjection;
using TheLair.ASP_Net.Serializers;

namespace TheLair.ASP_Net.Helpers;

public static class OneOfHelper
{
    public static IMvcBuilder ConfigureOneOf(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(o =>
        {
            o.JsonSerializerOptions.Converters.Add(new OneOfSerializer());
        });

        return (builder);
    }
}