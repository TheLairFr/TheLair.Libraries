using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheLair.ASP_Net.Exception;

namespace TheLair.ASP_Net.Middlewares;

public class NotAllowedMiddleware
{
    private readonly RequestDelegate _next;

    public NotAllowedMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (NotAllowedException)
        {
            context.Response.StatusCode = 403;
        }
    }
}