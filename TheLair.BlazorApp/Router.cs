using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace TheLair.BlazorApp;

public class Router
{
    private readonly NavigationManager NavigationManager;

    public string Uri => NavigationManager.Uri;

    public Router(NavigationManager navigationManager)
    {
        NavigationManager = navigationManager;
    }

    public void NavigateTo(string path, bool reload = false)
    {
        NavigationManager.NavigateTo(path, reload);
    }

    public string GetStringQueryParameter(string parameter)
    {
        if (!NavigationManager.Uri.Contains("?"))
            return (string.Empty);

        if (!QueryHelpers.ParseQuery(NavigationManager.Uri.Split("?")[1]).TryGetValue(parameter, out StringValues value))
            return String.Empty;

        Console.WriteLine("found");
        return (value.ToString());
    }
}