namespace TheLair.Database;

public class NewOrExistingEntity<T> where T : Entity<T>
{
    public T Entity;
    private readonly Action<T> PersistAction;

    public NewOrExistingEntity(T entity, Action<T> persist)
    {
        Entity = entity;
        PersistAction = persist;
    }

    public void Persist()
    {
        PersistAction(Entity);
    }
}