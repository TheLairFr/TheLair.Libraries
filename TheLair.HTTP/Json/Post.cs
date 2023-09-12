using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.HTTP.Json;

public partial class JsonHttpClient
{
    public Task<Response> Post<T>(string url, T instance)
        where T : class
    {
        HttpContent content = PrepareContent(instance);

        return (InternalExceptionHandler(() => Client.PostAsync(url, content)));
    }

    public Task<Response<T>> Post<T, U>(string url, U instance) 
        where T : class 
        where U : class 
    {
        HttpContent content = PrepareContent(instance);

        return (InternalExceptionHandler<T>(() => Client.PostAsync(url, content)));
    }
}