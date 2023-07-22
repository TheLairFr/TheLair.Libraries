using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TheLair.JWT;

namespace TheLair.JWT.Helpers;

public static class ManualJWTServiceExtensions
{
    public static void AddManualJWTService<TService, TToken>(this IServiceCollection services) where TService : ManualJWTService<TToken>
    {
        services.AddScoped<TService>();
        services.AddSingleton(i =>
        {
            IConfiguration config = i.GetService<IConfiguration>()!;

            return new JWTConfig(config["Jwt:Issuer"]!, config["Jwt:Audience"]!, config["Jwt:Key"]!);
        });
    }

    public static void AddManualJWTService(this IServiceCollection services)
    {
        services.AddManualJWTService<ManualJWTService<JWTFrame>, JWTFrame>();
    }

    public static void UseJWTCookie<TService, TToken>(this WebApplication app) where TService : ManualJWTService<TToken>
    {
        app.Use(Middleware<TService, TToken>);
    }

    public static void UseJWTCookie(this WebApplication app)
    {
        app.Use(Middleware<ManualJWTService<JWTFrame>, JWTFrame>);
    }

    private static Task Middleware<TService, TToken>(HttpContext ctx, RequestDelegate next) where TService : ManualJWTService<TToken>
    {
        TService service = ctx.RequestServices.GetService<TService>()!;

        if (ctx.Request.Cookies.TryGetValue("JWT", out string? found))
            service.VerifyToken(found);

        return next(ctx);
    }
}