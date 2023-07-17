using Microsoft.Extensions.DependencyInjection;

namespace TheLair.Database.Helpers;

public static class RepositoryHelper
{
    public static void AddRepositoriesFromSameAssemblyOf<T>(this IServiceCollection service)
    {
        Type[] types = typeof(T).Assembly.GetTypes()
            .Where(i => !i.IsAbstract && i.IsAssignableTo(typeof(Repository)))
            .ToArray();

        foreach (Type type in types)
        {
            service.AddScoped(type);
        }
    }
}