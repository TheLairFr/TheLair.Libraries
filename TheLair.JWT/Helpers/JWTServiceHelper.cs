using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace TheLair.JWT.Helpers;

public static class JWTServiceHelper
{
    public static void AddJWTService(this IServiceCollection services, WebApplicationBuilder builder)
    {
        services.AddAuthorization();
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true
            };
        });

        services.AddSingleton<JWTConfig>(i =>
        {
            IConfiguration config = i.GetRequiredService<IConfiguration>();

            try
            {
                return new JWTConfig(config["Jwt:Issuer"]!, config["Jwt:Audience"]!, config["Jwt:Key"]!);
            }
            catch (Exception)
            {
                Console.WriteLine("Missing Configuration for JWT !");
                Console.WriteLine("Please use this section in appsettings.json:");
                Console.WriteLine("""
                                  "JWT": {
                                    "Issuer": "[Issuer]",
                                    "Audience": "[Audience]",
                                    "Key": "S4lTC0d3123456789123456789_"
                                  }
                                  """);
                throw;
            }
            
        });
        services.AddScoped<JWTService>();
    }
}