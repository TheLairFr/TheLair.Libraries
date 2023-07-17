using Microsoft.AspNetCore.Builder;
using TheLair.ASP_Net.Middlewares;

namespace TheLair.ASP_Net.Helpers;

public static class EnhancedResponses
{
    public static void UseNotFound(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<NotFoundMiddleware>();
    }

    public static void UseNotAllowed(this IApplicationBuilder builder)
    {
        builder.UseMiddleware<NotAllowedMiddleware>();
    }
}