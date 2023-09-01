using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.Extensions.DependencyInjection;
using TheLair.BlazorApp.StateManagement;

namespace TheLair.BlazorApp;

public static class DependencyInjectionExtentions
{
    public static void AddTheLairBlazorApp<TStateManager>(this IServiceCollection collection)
        where TStateManager : class, IStateManager

    {
        collection.AddBlazoredLocalStorageAsSingleton();
        collection.AddSingleton<TStateManager>();
        collection.AddScoped<Router>();
    }

    public static void AddTheLairBlazorApp<TStateManager, TState>(this IServiceCollection collection)
        where TStateManager : class, IStateManager
        where TState : class

    {
        collection.AddBlazoredLocalStorageAsSingleton();
        collection.AddSingleton<TStateManager>();
        collection.AddSingleton<TState>();
        collection.AddScoped<Router>();
    }

    public static void AddTheLairBlazorApp<TStateManager, TState, TSession>(this IServiceCollection collection)
        where TStateManager : SessionStateManager<TState, TSession>
        where TState : class
        where TSession : class

    {
        collection.AddBlazoredLocalStorageAsSingleton();
        collection.AddSingleton<TStateManager>();
        collection.AddSingleton<TState>();
        collection.AddSingleton<TSession>();
        collection.AddScoped<Router>();
    }
}