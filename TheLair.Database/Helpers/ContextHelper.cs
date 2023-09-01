using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.DependencyInjection;

namespace TheLair.Database.Helpers;

public static class ContextHelper
{
    public static void AddContext<T>(this IServiceCollection service, Func<IServiceProvider, T> fct) where T : BDDContext
    {
        service.AddSingleton<MigrationInfo<T>>();
        service.AddScoped(i =>
        {
            MigrationInfo<T> migration = i.GetService<MigrationInfo<T>>()!;
            T context = fct(i);
            
            if (!migration.Migated)
            {
                migration.Migated = true;
                context.Database.Migrate();
            }

            return (context);
        });
    }
}