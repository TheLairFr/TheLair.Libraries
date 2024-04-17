using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.HTTP.Json;

public partial class JsonHttpClient
{

    protected Task<Response> Patch<T>(string url, T instance) where T : class
    {
        HttpContent content = PrepareContent(instance);

        return (InternalExceptionHandler(() => Client.PatchAsync(url, content)));
    }

    protected Task<Response<T>> Patch<T, U>(string url, U instance)
        where T : class
        where U : class
    {
        HttpContent content = PrepareContent(instance);

        return (InternalExceptionHandler<T>(() => Client.PatchAsync(url, content)));
    }
}