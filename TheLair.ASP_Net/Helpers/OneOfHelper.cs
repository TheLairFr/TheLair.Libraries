using Microsoft.Extensions.DependencyInjection;
using TheLair.ASP_Net.OneOfLogic;
using TheLair.ASP_Net.Serializers;

namespace TheLair.ASP_Net.Helpers;

public static class OneOfHelper
{
    public static IServiceCollection ConfigureOneOf(this IServiceCollection services)
    {
        services.AddControllersWithViews(o => o.Filters.Add(new OneOfFilter()))
            .AddJsonOptions(o =>
            {
                o.JsonSerializerOptions.Converters.Add(new OneOfSerializer());
            });

        return (services);
    }
}