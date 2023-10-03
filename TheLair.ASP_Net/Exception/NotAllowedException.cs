namespace TheLair.ASP_Net.Exception;

public class NotAllowedException : CustomCodeException
{
    public NotAllowedException() : base(StatusCodes.Status403Forbidden) { }
}