using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TheLair.ASP_Net.Helpers;

public static class ConfigurationHelper
{
    public static void AddConfiguration<TConfigClass>(this IServiceCollection services, Action<TConfigClass>? fct = null, Func<TConfigClass, bool>? validator = null)
        where TConfigClass : class
    {
        services.AddOptions<TConfigClass>()
            .Configure(i => fct?.Invoke(i))
            .Validate(i => validator == null || validator.Invoke(i));
    }

    public static void MapConfigurationFromConfig<TConfigClass>(this IServiceCollection services, string configString) 
        where TConfigClass : class, new()
    {
        services.AddSingleton(i =>
        {
            IConfiguration configuration = i.GetRequiredService<IConfiguration>();
            IConfigurationSection section = configuration.GetSection(configString);
            TConfigClass options = new TConfigClass();
            section.Bind(options);

            return options;
        });
    }
}