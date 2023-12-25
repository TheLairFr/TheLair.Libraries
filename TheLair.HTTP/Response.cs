using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.HTTP;

public class Response
{
    public HttpStatusCode Code { get; set; }
    public Exception? Exception { get; set; }
    public HttpResponseMessage Message { get; set; }

    public Response() { }

    public Response(Exception e)
    {
        Exception = e;
    }

    public bool Success => (int)Code >= 200 && (int)Code <= 299;

    public static implicit operator bool(Response instance)
    {
        return (instance.Success);
    }
}

public class Response<T> : Response
{
    public T Content { get; set; } = default!;

    public Response() { }

    public Response(Exception e) : base(e) { }

    public static implicit operator T(Response<T> instance)
    {
        return (instance.Content);
    }
}