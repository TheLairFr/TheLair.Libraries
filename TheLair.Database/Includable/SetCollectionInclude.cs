using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace TheLair.Database.Includable;

public class SetCollectionInclude<TEntity, TSingle, TRepository>
    where TEntity : Entity<TEntity>
    where TRepository : Repository<TEntity>
{
    public readonly TRepository Repository;
    internal readonly IIncludableQueryable<TEntity, List<TSingle>> Queryable;

    public SetCollectionInclude(TRepository repo, IIncludableQueryable<TEntity, List<TSingle>> queryable)
    {
        Repository = repo;
        Queryable = queryable;
    }

    public SetInclude<TEntity, Target, TRepository> Include<Target>(Expression<Func<TEntity, Target>> expr)
    {
        return new SetInclude<TEntity, Target, TRepository>(Repository, Queryable.Include(expr));
    }

    public SetCollectionInclude<TEntity, Target, TRepository> Include<Target>(Expression<Func<TEntity, List<Target>>> expr)
    {
        return new SetCollectionInclude<TEntity, Target, TRepository>(Repository, Queryable.Include(expr));
    }

    public SetInclude<TEntity, Target, TRepository> ThenInclude<Target>(Expression<Func<TSingle, Target>> expr)
    {
        return new SetInclude<TEntity, Target, TRepository>(Repository, Queryable.ThenInclude(expr));
    }

    public SetCollectionInclude<TEntity, Target, TRepository> ThenInclude<Target>(Expression<Func<TSingle, List<Target>>> expr)
    {
        return new SetCollectionInclude<TEntity, Target, TRepository>(Repository, Queryable.ThenInclude(expr));
    }

    public TRepository Apply()
    {
        Repository.ApplySet(Queryable);
        return Repository;
    }
}