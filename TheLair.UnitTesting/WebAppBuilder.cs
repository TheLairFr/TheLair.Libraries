using Microsoft.AspNetCore.Builder;

namespace TheLair.UnitTesting;

public static class WebAppBuilder
{
    public static WebApplication SetupApplication(Action<WebApplicationBuilder>[] builders = new Action<WebApplicationBuilder>[]
    {

    })
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder();


    }
}