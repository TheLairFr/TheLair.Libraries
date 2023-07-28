namespace TheLair.Database;

public static class NewOrExistingEntity
{
    public static NewOrExistingEntity<T> Add<T>(T? entity, Repository<T> repository) where T : Entity<T>
    {
        return (new NewOrExistingEntity<T>(entity!, i => repository.Add(i)));
    }

    public static NewOrExistingEntity<T> Update<T>(T? entity, Repository<T> repository) where T : Entity<T>
    {
        return (new NewOrExistingEntity<T>(entity!, repository.Update));
    }
}

public class NewOrExistingEntity<T> where T : Entity<T>
{
    public T Entity;
    private readonly Action<T> PersistAction;

    public NewOrExistingEntity(T entity, Action<T> persist)
    {
        Entity = entity;
        PersistAction = persist;
    }

    public void Perist()
    {
        PersistAction(Entity);
    }
}