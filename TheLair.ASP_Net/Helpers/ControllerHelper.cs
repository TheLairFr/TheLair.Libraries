using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace TheLair.ASP_Net.Helpers;

public static class ControllerHelper
{
    public static void AddControllersFromSameAssemblyOf<T>(this IServiceCollection service)
    {
        Type[] types = typeof(T).Assembly.GetTypes()
            .Where(i => !i.IsAbstract && i.IsAssignableTo(typeof(Controller)))
            .ToArray();

        foreach (Type type in types)
        {
            service.AddScoped(type);
        }
    }
}