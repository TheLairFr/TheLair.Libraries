using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TheLair.Database;
using TheLair.Database.Includable;

namespace TheLair.Database;

public abstract class Repository
{

}

public abstract class Repository<TEntity, TRepository, TContext> : Repository, IRepository<TEntity>
    where TEntity : Entity<TEntity>
    where TRepository : Repository<TEntity, TRepository, TContext>
    where TContext : BDDContext<TContext>
{
    private readonly TContext Context;
    private DbSet<TEntity> InnerSet = null!;
    protected IQueryable<TEntity> Set = null!;
    
    protected Repository(TContext context)
    {
        Context = context;
    }

    protected void UseSet(DbSet<TEntity> set)
    {
        InnerSet = set;
        Set = InnerSet.Where(i => i.Enabled);
    }

    public TEntity Add(TEntity entity)
    {
        InnerSet.Add(entity);
        
        return (entity);
    }

    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
    {
        InnerSet.AddRange(entities);

        return (entities);
    }

    public void Update(TEntity entity)
    {
        entity.Modified = DateTime.Now;

        InnerSet.Update(entity);
    }

    public void UpdateRange(IEnumerable<TEntity> entities)
    {
        foreach (TEntity entity in entities)
        {
            Update(entity);
        }
    }

    protected virtual void InnerDelete(TEntity entity) { }

    public void Delete(TEntity entity, bool hardDelete = false)
    {
        InnerDelete(entity);

        if (!hardDelete)
        {
            entity.Enabled = false;
            
            Update(entity);
        }
        else
        {
            InnerSet.Remove(entity);
        }
    }

    public void DeleteRange(IEnumerable<TEntity> entities, bool hardDelete = false)
    {
        foreach (TEntity entity in entities)
        {
            Delete(entity, hardDelete);
        }
    }

    public void Save()
    {
        Context.SaveChanges();
    }

    public Task SaveAsync()
    {
        return (Context.SaveChangesAsync());
    }

    public SetInclude<TEntity, TTargetEntity, TRepository> Include<TTargetEntity>(Expression<Func<TEntity, TTargetEntity>> expr)
    {
        return (new SetInclude<TEntity, TTargetEntity, TRepository>((TRepository)this, Set.Include(expr)));
    }

    public SetCollectionInclude<TEntity, TTargetEntity, TRepository> Include<TTargetEntity>(Expression<Func<TEntity, List<TTargetEntity>>> expr)
    {
        return (new SetCollectionInclude<TEntity, TTargetEntity, TRepository>((TRepository)this, Set.Include(expr)));
    }

    public TEntity WithId(Guid id)
    {
        return (Set.First(i => i.Id == id));
    }

    public TEntity? WithIdOrNull(Guid id)
    {
        return (Set.FirstOrDefault(i => i.Id == id));
    }

    public void ApplySet(IQueryable<TEntity> set)
    {
        Set = set;
    }
}