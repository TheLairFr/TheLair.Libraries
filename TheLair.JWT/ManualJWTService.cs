using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace TheLair.JWT;

public class ManualJWTService<TToken> : BaseJWTService
{
    public TToken? JWTValues;
    protected bool Authenticated;
    
    public ManualJWTService(JWTConfig config) : base(config) { }

    public bool IsAuthenticated()
    {
        return (Authenticated);
    }

    public bool VerifyToken(string token)
    {
        try
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(Config.Key);

            if (!string.IsNullOrEmpty(token))
            {
                TokenValidationParameters tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken? securityToken);
                JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

                if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                    return (false);

                JWTValues = ExtractTokenValues<TToken>(token);
                Authenticated = (JWTValues != null);

                return (Authenticated);
            }
        }
        catch (SecurityTokenException)
        {
            return (false);
        }
        

        return (false);
    }
}