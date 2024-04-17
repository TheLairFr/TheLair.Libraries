using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.HTTP.Json;

public partial class JsonHttpClient
{

    protected Task<Response> Put<T>(string url, T instance) 
        where T : class
    {
        HttpContent content = PrepareContent(instance);

        return (InternalExceptionHandler(() => Client.PutAsync(url, content)));
    }

    protected Task<Response<T>> Put<T, U>(string url, U instance)
        where T : class
        where U : class
    {
        HttpContent content = PrepareContent(instance);

        return (InternalExceptionHandler<T>(() => Client.PutAsync(url, content)));
    }
}