namespace TheLair.ASP_Net;

public abstract class OneOf
{
    public object Value;

    protected OneOf(object value)
    {
        Value = value;
    }

    public object GetValue()
    {
        return (Value);
    }
}

public class OneOf<T0, T1> : OneOf
{
    public OneOf(object value) : base(value) { }

    public static implicit operator OneOf<T0, T1>(T0 value) => new(value!);
    public static implicit operator OneOf<T0, T1>(T1 value) => new(value!);
}

public class OneOf<T0, T1, T2> : OneOf
{
    public OneOf(object value) : base(value) { }

    public static implicit operator OneOf<T0, T1, T2>(T0 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2>(T1 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2>(T2 value) => new(value!);
}

public class OneOf<T0, T1, T2, T3> : OneOf
{
    public OneOf(object value) : base(value) { }

    public static implicit operator OneOf<T0, T1, T2, T3>(T0 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3>(T1 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3>(T2 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3>(T3 value) => new(value!);
}

public class OneOf<T0, T1, T2, T3, T4> : OneOf
{
    public OneOf(object value) : base(value) { }

    public static implicit operator OneOf<T0, T1, T2, T3, T4>(T0 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4>(T1 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4>(T2 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4>(T3 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4>(T4 value) => new(value!);
}

public class OneOf<T0, T1, T2, T3, T4, T5> : OneOf
{
    public OneOf(object value) : base(value) { }

    public static implicit operator OneOf<T0, T1, T2, T3, T4, T5>(T0 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4, T5>(T1 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4, T5>(T2 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4, T5>(T3 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4, T5>(T4 value) => new(value!);
    public static implicit operator OneOf<T0, T1, T2, T3, T4, T5>(T5 value) => new(value!);
}