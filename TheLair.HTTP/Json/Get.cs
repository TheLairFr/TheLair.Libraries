using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.HTTP.Json;

public partial class JsonHttpClient
{
    public Task<Response<string>> GetAsString(string url)
    {
        return (InternalExceptionHandler(() => Client.GetAsync(url), async r => await r.Content.ReadAsStringAsync()));
    }

    public Task<Response> Get(string url)
    {
        return (InternalExceptionHandler(() => Client.GetAsync(url)));
    }

    public Task<Response<T>> Get<T>(string url) 
        where T : class
    {
        return (InternalExceptionHandler<T>(() => Client.GetAsync(url)));
    }
}