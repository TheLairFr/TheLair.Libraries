namespace TheLair.Extensions.Task;

public static class Waiter
{
    public static async System.Threading.Tasks.Task Wait<T0, T1>(Task<T0> t0, Task<T1> t1, Action<T0, T1> fct)
    {
        fct(await t0, await t1);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1, T2>(Task<T0> t0, Task<T1> t1, Task<T2> t2, Action<T0, T1, T2> fct)
    {
        fct(await t0, await t1, await t2);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1, T2, T3>(Task<T0> t0, Task<T1> t1, Task<T2> t2, Task<T3> t3, Action<T0, T1, T2, T3> fct)
    {
        fct(await t0, await t1, await t2, await t3);
    }

    public static async System.Threading.Tasks.Task Wait<T0, T1, T2, T3, T4>(Task<T0> t0, Task<T1> t1, Task<T2> t2, Task<T3> t3, Task<T4> t4, Action<T0, T1, T2, T3, T4> fct)
    {
        fct(await t0, await t1, await t2, await t3, await t4);
    }
}