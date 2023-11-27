using System.Data;

namespace TheLair.BlazorApp.Claims;

public class ClaimChallenger<T>
{
    public List<ClaimChallenge<T>> ClaimChallenges { get; set; } = new List<ClaimChallenge<T>>();

    public string FallbackMessage = "";

    public void RequireRole(T role)
    {
        ClaimChallenges.Add(new ClaimChallenge<T>
        {
            Mandatory = false,
            Challenge = r => r.Any(o => o!.Equals(role))
        });
    }

    public void RequireCustom(Func<IEnumerable<T>, bool> validation)
    {
        ClaimChallenges.Add(new ClaimChallenge<T>
        {
            Mandatory = false,
            Challenge = validation
        });
    }

    public void RequireNoRole()
    {
        ClaimChallenges.Add(new ClaimChallenge<T>
        {
            Mandatory = false,
            Challenge = r => true
        });
    }

    public void RequireMandatoryRole(T role, string message = "")
    {
        ClaimChallenges.Add(new ClaimChallenge<T>
        {
            Mandatory = true,
            Challenge = r => r.Any(o => o!.Equals(role)),
            Message = message
        });
    }

    public void RequireMandatoryCustom(Func<IEnumerable<T>, bool> validator, string message = "")
    {
        ClaimChallenges.Add(new ClaimChallenge<T>
        {
            Mandatory = true,
            Challenge = validator,
            Message = message
        });
    }

    public ChallengeResult Challenge(IEnumerable<T> roles)
    {
        if (!ClaimChallenges.Any())
            throw new Exception("No Claim Challenge Defined for scope !!!");

        IGrouping<bool, ClaimChallenge<T>>[] challenges = ClaimChallenges
            .GroupBy(i => i.Mandatory)
            .ToArray();

        string[] messages = challenges
            .Where(i => i.Key)
            .SelectMany(i => i)
            .Where(i => !i.Challenge(roles))
            .Select(i => i.Message)
            .ToArray();

        if (messages.Any())
            return (new ChallengeResult
            {
                Success = false,
                Errors = messages
            });

        IEnumerable<ClaimChallenge<T>> found = challenges.Where(i => !i.Key)
            .SelectMany(i => i)
            .ToArray();

        if (!found.Any() || found.Any(i => i.Challenge(roles)))
            return (new ChallengeResult
            {
                Success = true
            });

        return (new ChallengeResult
        {
            Success = false,
            Errors = new[] { FallbackMessage } 
        });
    }
}