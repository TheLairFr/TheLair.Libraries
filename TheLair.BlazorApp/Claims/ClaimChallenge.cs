namespace TheLair.BlazorApp.Claims;

public class ClaimChallenge<T>
{
    public required bool Mandatory;
    public required Func<IEnumerable<T>, bool> Challenge;
    public string Message = "";
}