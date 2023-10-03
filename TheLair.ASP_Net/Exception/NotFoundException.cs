namespace TheLair.ASP_Net.Exception;

public class NotFoundException : CustomCodeException
{
    public NotFoundException() : base(StatusCodes.Status404NotFound) { }
}