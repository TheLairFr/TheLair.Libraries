namespace TheLair.ASP_Net.OneOfLogic.StatusCodes;

public class OneOfStatusCode
{
    public int Code;

    public OneOfStatusCode(int code)
    {
        Code = code;
    }
}

public static class OneOfStatusCodeCache
{
    public static Forbidden Forbidden = new Forbidden();
    public static NotFound NotFound = new NotFound();
}

public class Forbidden : OneOfStatusCode
{
    public Forbidden() : base(403) { }
}

public class NotFound : OneOfStatusCode
{
    public NotFound() : base(404) { }
}