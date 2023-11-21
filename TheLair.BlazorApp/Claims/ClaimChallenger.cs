namespace TheLair.BlazorApp.Claims;

public class ClaimChallenger<T>
{
    public List<Func<IEnumerable<T>, bool>> ClaimChallenges { get; set; } = new List<Func<IEnumerable<T>, bool>>();

    public void RequireRole(T role)
    {
        ClaimChallenges.Add(r => r.Any(o => o.Equals(role)));
    }

    public void RequireCustom(Func<IEnumerable<T>, bool> validation)
    {
        ClaimChallenges.Add(validation);
    }

    public void RequireNoRole()
    {
        ClaimChallenges.Add(r => true);
    }

    public bool Challenge(IEnumerable<T> roles)
    {
        if (!ClaimChallenges.Any())
            throw new Exception("No Claim Challenge Defined for scope !!!");

        return (ClaimChallenges.Any(o => o(roles)));
    }
}