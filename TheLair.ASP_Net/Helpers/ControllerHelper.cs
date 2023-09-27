using Microsoft.AspNetCore.Mvc;

namespace TheLair.ASP_Net.Helpers;

public static class ControllerHelper
{
    public static void AddControllersFromSameAssemblyOf<T>(this IServiceCollection service)
    {
        Type[] types = typeof(T).Assembly.GetTypes()
            .Where(i => !i.IsAbstract && i.IsAssignableTo(typeof(ControllerBase)))
            .ToArray();

        foreach (Type type in types)
        {
            service.AddScoped(type);
        }
    }
}