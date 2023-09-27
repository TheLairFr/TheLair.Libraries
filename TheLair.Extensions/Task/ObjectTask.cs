namespace TheLair.Extensions.Task;

public class ObjectTask<T>
{
    public T Value;
    public System.Threading.Tasks.Task Task;

    public ObjectTask(T value, System.Threading.Tasks.Task task)
    {
        Value = value;
        Task = task;
    }
}