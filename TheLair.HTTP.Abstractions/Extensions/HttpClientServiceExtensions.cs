using Microsoft.Extensions.DependencyInjection;
using TheLair.HTTP.Json;

namespace TheLair.HTTP.Abstractions.Extensions;

public static class HttpClientServiceExtensions
{
    public static void AddJsonClient<T>(this IServiceCollection services, string url) where T : JsonHttpClient, new()
    {
        services.AddScoped(i =>
        {
            T instance = new T();

            instance.GetInnerClient().BaseAddress = new Uri(url);

            return (instance);
        });
    }
}
