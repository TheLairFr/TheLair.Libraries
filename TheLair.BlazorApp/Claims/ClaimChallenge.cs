namespace TheLair.BlazorApp.Claims;

public class ClaimChallenge<T>
{
    public required Func<IEnumerable<T>, bool> Challenge;
}