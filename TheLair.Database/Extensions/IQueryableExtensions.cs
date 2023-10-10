using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.Database.Extensions;

public static class IQueryableExtensions
{
    public static NewOrExistingEntity<T> RetrieveOrCreate<T>(this IQueryable<T> queryable, Repository<T> builder) where T : Entity<T>, new()
    {
        T? found = queryable.FirstOrDefault();

        return (found != null 
            ? builder.ExistingEntity(found) 
            : builder.NewEntity(new()));
    }

    public static NewOrExistingEntity<T> RetrieveOrCreate<T>(this IQueryable<T> queryable, Repository<T> builder, Func<T> factory) where T : Entity<T>, new()
    {
        T? found = queryable.FirstOrDefault();

        return (found != null 
            ? builder.ExistingEntity(found) 
            : builder.NewEntity(factory()));
    }
}