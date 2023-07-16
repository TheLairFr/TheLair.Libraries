﻿using System;
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
    public static void AddStateManager<TStateManager>(this IServiceCollection collection) 
        where TStateManager : class, IStateManager
    {
        collection.AddBlazoredLocalStorageAsSingleton();
        collection.AddSingleton<TStateManager>();
    }
}