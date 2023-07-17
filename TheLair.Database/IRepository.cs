namespace TheLair.Database;

public interface IRepository<in T>
{
    void ApplySet(IQueryable<T> set);
}