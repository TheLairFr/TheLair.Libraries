using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace TheLair.CLI.Commands.API;

public class MethodRoute
{
    public required Type ControllerType;
    public required RouteAttribute? Route;
    public required MethodInfo Method;

    private static Type[] Verbs = new Type[]
    {
        typeof(HttpGetAttribute),
        typeof(HttpPostAttribute),
        typeof(HttpPutAttribute),
        typeof(HttpPatchAttribute),
        typeof(HttpDeleteAttribute),
    };

    public string GenMethodCall()
    {
        APIRoute? verb = ExtractVerb();
        
        if (verb == null)
            return "";

        string controllerName = ControllerType.Name.Replace("Controller", "");
        StringBuilder b = new StringBuilder();

        b.Append($"public {GenReturn()} {controllerName}_{verb.Verb}_{Method.Name}(");
        b.Append($")\n{{\n\treturn ({verb.Verb}(\"{GenRouteURL(controllerName, verb)}\"));\n}}\n\n");
        string r = b.ToString();

        return (r);
    }

    private string GenReturn()
    {
        string name = Method.ReturnType.Name;
        int idx = name.IndexOf('`');

        if (idx != -1)
            name = name.Substring(0, idx);

        if (Method.ReturnType.GetGenericArguments().Length > 0)
            name += "<" + string.Join(',', Method.ReturnType.GetGenericArguments().Select(i => i.Name).ToArray()) + ">";

        return (name);
    }

    public string GenRouteURL(string controllerName, APIRoute apiRoute)
    {
        StringBuilder builder = new StringBuilder();

        builder.Append(Route != null ? Route.Template.Replace("[Controller]", controllerName).Replace("[controller]", controllerName) : "");
        string a = apiRoute.Url;

        while (a.StartsWith("/"))
        {
            a = a.Substring(1);
        }

        builder.Append('/');
        builder.Append(a);

        return (builder.ToString());
    }

    private APIRoute? ExtractVerb()
    {
        foreach (Type verb in Verbs)
        {
            if (Method.GetCustomAttribute(verb) is HttpMethodAttribute found)
            {
                return new APIRoute
                {
                    Verb = found.GetType().Name.Substring(4).Replace("Attribute", ""),
                    Url = found.Template ?? ""
                };
            }
        }

        return (null);
    }
}