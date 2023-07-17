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
        string issuer = Config.Issuer;
        string audience = Config.Audience;
        byte[] key = Encoding.ASCII.GetBytes(Config.Key);
        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(),
            Expires = DateTime.UtcNow.AddMinutes(60),
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