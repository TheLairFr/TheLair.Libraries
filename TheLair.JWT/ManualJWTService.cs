using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace TheLair.JWT;

public class ManualJWTService<TToken>
{
    private readonly JWTConfig Configuration;

    public TToken? JWTValues;
    protected bool Authenticated;

    public ManualJWTService(JWTConfig config)
    {
        Configuration = config;
    }

    public bool IsAuthenticated()
    {
        return (Authenticated);
    }

    public bool VerifyToken(string token)
    {
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        byte[] key = Encoding.ASCII.GetBytes(Configuration.Key);
        
        if (!string.IsNullOrEmpty(token))
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };

            tokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);
            JwtSecurityToken? jwtSecurityToken = securityToken as JwtSecurityToken;

            if (jwtSecurityToken == null || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512, StringComparison.InvariantCultureIgnoreCase))
                return (false);

            Authenticated = ExtractTokenValues(token);

            return (Authenticated);
        }

        return (false);
    }

    private bool ExtractTokenValues(string token)
    {
        string base64String = token.Split('.')[1];
        int length = base64String.Length % 4;

        base64String = length switch
        {
            2 => $"{base64String}==",
            3 => $"{base64String}=",
            _ => base64String
        };

        byte[] data = Convert.FromBase64String(base64String);
        string decodedString = Encoding.UTF8.GetString(data);

        JWTValues = JsonConvert.DeserializeObject<TToken>(decodedString)!;

        return (JWTValues != null);
    }

    public string Forge(object claims)
    {
        return (JWTService.InnerForge(claims, Configuration));
    }
}