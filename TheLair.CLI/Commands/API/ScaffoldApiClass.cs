using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TheLair.CLI.Commands.API;

public class ScaffoldApiClass : CommandArgumentExec
{
    public ScaffoldApiClass() : base("api", TryExec) { }

    public static void TryExec(string[] args)
    {
        DirectoryInfo dir = Directory.GetParent(args[1]);
        FileInfo[] libs = dir.GetFiles("*.dll");

        foreach (FileInfo lib in libs)
        {
            try
            {
                Assembly.LoadFrom(lib.FullName);
            }
            catch (Exception)
            {
            }
        }

        Assembly assembly = Assembly.LoadFrom(args[1]);
        Type[] found = assembly.GetTypes()
            .Where(i => !i.IsAbstract && i.IsAssignableTo(typeof(ControllerBase)))
            .ToArray();

        MethodRoute[] results = found.SelectMany(i =>
        {
            RouteAttribute? route = i.GetCustomAttribute<RouteAttribute>();

            IEnumerable<MethodInfo> methods = i.GetMethods().Where(i => i.GetCustomAttributes().Count() >= 0);

            return methods.Select(o => new MethodRoute
            {
                ControllerType = i,
                Route = route,
                Method = o
            });
        }).ToArray();

        StringBuilder builder = new StringBuilder();

        foreach (MethodRoute result in results)
        {
            builder.Append(result.GenMethodCall());
        }

        File.WriteAllText("output.cs.out", builder.ToString());
    }
}