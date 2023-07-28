using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.Extensions.Tasks;

public class ObjectTask<T>
{
    public T Value;
    public Task Task;

    public ObjectTask(T value, Task task)
    {
        Value = value;
        Task = task;
    }
}