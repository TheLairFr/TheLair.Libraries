using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.HTTP.Json;

public partial class JsonHttpClient
{

    protected Task<Response> Delete(string url)
    {
        return (InternalExceptionHandler(() => Client.DeleteAsync(url)));
    }

    protected Task<Response<T>> Delete<T>(string url) 
        where T : class
    {
        return (InternalExceptionHandler<T>(() => Client.DeleteAsync(url)));
    }
}