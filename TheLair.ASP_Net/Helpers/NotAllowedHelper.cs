using Microsoft.AspNetCore.Mvc;
using TheLair.ASP_Net.Exception;

namespace TheLair.ASP_Net.Helpers;

public static class CustomCodesHelper
{
    public static IApplicationBuilder UseCustomCodes(this IApplicationBuilder builder)
    {
        builder.Use(async (i, j) =>
        {
            try
            {
                await j.Invoke();
            }
            catch (CustomCodeException ex)
            {
                i.Response.StatusCode = ex.Code;
            }
        });

        return (builder);
    }
}