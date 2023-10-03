using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TheLair.Extensions.Object;

namespace TheLair.JWT;

public class JWTService
{
    private readonly JWTConfig Config;

    public JWTService(JWTConfig config)
    {
        Config = config;
    }

    public string Forge(object claims)
    {
        return (InnerForge(claims, Config));
    }

    internal static string InnerForge(object claims, JWTConfig config)
    {
        string issuer = config.Issuer;
        string audience = config.Audience;
        byte[] key = Encoding.ASCII.GetBytes(config.Key);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(),
            Expires = DateTime.UtcNow.AddMinutes(config.MinutesOfValidity),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature),
            Claims = claims.ObjectToDictionnary()
        };
        JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
        SecurityToken? token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}