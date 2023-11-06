namespace TheLair.Extensions.Task;

public static class Waiter
{
    public static async System.Threading.Tasks.Task Wait<T0, T1>(Task<T0> t0, Task<T1> t1, Action<T0, T1> fct)
    {
        fct(await t0, await t1);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1>(Task<T0> t0, Action<T0> t0Setter, Task<T1> t1, Action<T1> t1Setter)
    {
        t0Setter(await t0);
        t1Setter(await t1);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1, T2>(Task<T0> t0, Task<T1> t1, Task<T2> t2, Action<T0, T1, T2> fct)
    {
        fct(await t0, await t1, await t2);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1, T2>(Task<T0> t0, Action<T0> t0Setter,
        Task<T1> t1, Action<T1> t1Setter, 
        Task<T2> t2, Action<T2> t2Setter)
    {
        t0Setter(await t0);
        t1Setter(await t1);
        t2Setter(await t2);
    }


    public static async System.Threading.Tasks.Task Wait<T0, T1, T2, T3>(Task<T0> t0, Task<T1> t1, Task<T2> t2, Task<T3> t3, Action<T0, T1, T2, T3> fct)
    {
        fct(await t0, await t1, await t2, await t3);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1, T2, T3>(Task<T0> t0, Action<T0> t0Setter, 
        Task<T1> t1, Action<T1> t1Setter, 
        Task<T2> t2, Action<T2> t2Setter,
        Task<T3> t3, Action<T3> t3Setter)
    {
        t0Setter(await t0);
        t1Setter(await t1);
        t2Setter(await t2);
        t3Setter(await t3);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1, T2, T3, T4>(Task<T0> t0, Task<T1> t1, Task<T2> t2, Task<T3> t3, Task<T4> t4, Action<T0, T1, T2, T3, T4> fct)
    {
        fct(await t0, await t1, await t2, await t3, await t4);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1, T2, T3, T4>(Task<T0> t0, Action<T0> t0Setter,
        Task<T1> t1, Action<T1> t1Setter, 
        Task<T2> t2, Action<T2> t2Setter, 
        Task<T3> t3, Action<T3> t3Setter, 
        Task<T4> t4, Action<T4> t4Setter)
    {
        t0Setter(await t0);
        t1Setter(await t1);
        t2Setter(await t2);
        t3Setter(await t3);
        t4Setter(await t4);
    }

    public static Task<U> FromObject<T, U>(T obj, Func<T, Task<U>> resolver)
    {
        return (resolver(obj));
    }

    public static System.Threading.Tasks.Task FromObject<T>(T obj, Func<T, System.Threading.Tasks.Task> resolver)
    {
        return (resolver(obj));
    }

    public static async Task<V[]> FromObjects<T, U, V>(IEnumerable<T> collection, Func<T, U> method, Func<U, Task<V>> resolver)
    {
        IEnumerable<Task<V>> e = collection.Select(method).Select(resolver);

        return (await System.Threading.Tasks.Task.WhenAll(e));
    }
}