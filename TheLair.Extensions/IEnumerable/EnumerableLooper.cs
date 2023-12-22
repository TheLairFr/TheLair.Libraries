using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.Extensions.IEnumerable;

public class EnumerableLooper<T>
{
    private readonly T[] Enumerable;
    private int Idx;

    public EnumerableLooper(IEnumerable<T> enumerable)
    {
        Enumerable = enumerable.ToArray();
    }

    public T Take()
    {
        if (Idx >= Enumerable.Length)
            Idx = 0;

        return (Enumerable[Idx++]);
    }

    private IEnumerable<T> InnerTake(int count)
    {
        while (count > 0)
        {
            yield return (Take());
            --count;
        }
    }

    public T[] Take(int count)
    {
        return (InnerTake(count).ToArray());
    }

    public IEnumerable<T> TakeRemaining()
    {
        return (Take(Enumerable.Length - Idx));
    }

    public IEnumerable<T> TakeRaw()
    {
        return (Enumerable);
    }

    public void Reset()
    {
        Idx = 0;
    }
}