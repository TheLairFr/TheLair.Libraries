namespace TheLair.ASP_Net.Exception;

public class CustomCodeException : System.Exception
{
    public int Code;

    public CustomCodeException(int code)
    {
        Code = code;
    }
}