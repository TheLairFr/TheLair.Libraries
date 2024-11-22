using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TheLair.Database.Includable;

namespace TheLair.Database;

public abstract class Repository { }

public abstract class Repository<TEntity> : Repository
    where TEntity : Entity<TEntity>
{
    private DbSet<TEntity> InnerSet = null!;
    protected IQueryable<TEntity> Set = null!;

    protected void UseSet(DbSet<TEntity> set)
    {
        InnerSet = set;
        Set = InnerSet.Where(i => i.Enabled);
    }

    public IQueryable<TEntity> All()
    {
        return (Set);
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

    public void ApplySet(IQueryable<TEntity> set)
    {
        Set = set;
    }

    public NewOrExistingEntity<TEntity> NewEntity(TEntity entity)
    {
        return (new NewOrExistingEntity<TEntity>(entity, i => Add(i)));
    }

    public NewOrExistingEntity<TEntity> ExistingEntity(TEntity entity)
    {
        return (new NewOrExistingEntity<TEntity>(entity, Update));
    }
}

public abstract class Repository<TEntity, TRepository, TContext> : Repository<TEntity>
    where TEntity : Entity<TEntity>, new()
    where TRepository : Repository<TEntity, TRepository, TContext>
    where TContext : BDDContext<TContext>
{
    private readonly TContext Context;
    
    protected Repository(TContext context)
    {
        Context = context;
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

    public IQueryable<TEntity> WithIdAsQueryable(Guid id)
    {
        return (Set
            .Where(i => i.Id == id)
            .Take(1));
    }

    public IQueryable<TEntity> WithIdsAsQueryable(Guid[] id)
    {
        return (Set
            .Where(i => id.Contains(i.Id)));
    }

    public NewOrExistingEntity<TEntity> MakeNewOrExisting(Func<TEntity, bool> selector, Func<TEntity> factory)
    {
        TEntity? found = Set.FirstOrDefault(selector);

        return (found != null
            ? ExistingEntity(found)
            : NewEntity(factory()));
    }

    public NewOrExistingEntity<TEntity> MakeNewOrExisting(Guid id, Func<TEntity> factory)
    {
        TEntity? found = WithIdOrNull(id);

        return (found != null
            ? ExistingEntity(found)
            : NewEntity(factory()));
    }

    public NewOrExistingEntity<TEntity> MakeNewOrExisting(TEntity? entity, Func<TEntity> factory)
    {
        if (entity != null)
            return (ExistingEntity(entity));

        return (NewEntity(factory()));
    }
}