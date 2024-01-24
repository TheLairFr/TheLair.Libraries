using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TheLair.Extensions.Object;

namespace TheLair.JWT;

public class BaseJWTService
{
    protected readonly JWTConfig Config;

    public BaseJWTService(JWTConfig config)
    {
        Config = config;
    }

    public static T? InnerExtraction<T>(string token)
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

        return (JsonConvert.DeserializeObject<T>(decodedString));
    }

    public virtual T? ExtractTokenValues<T>(string token)
    {
        return (InnerExtraction<T>(token));
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
