using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace TheLair.HTTP.Json;

public partial class JsonHttpClient
{
    protected Task<Response> Post<T>(string url, T instance)
        where T : class
    {
        HttpContent content = PrepareContent(instance);

        return (InternalExceptionHandler(() => Client.PostAsync(url, content)));
    }

    protected Task<Response<T>> Post<T, U>(string url, U instance) 
        where T : class 
        where U : class 
    {
        HttpContent content = PrepareContent(instance);

        return (InternalExceptionHandler<T>(() => Client.PostAsync(url, content)));
    }

    protected Task<Response> PostFile<T>(string url, T instance, params FileContent[] files) where T : class
    {
        MultipartFormDataContent content = new MultipartFormDataContent();
        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");

        foreach (FileContent file in files)
        {
            content.Add(new StreamContent(file.Stream, Convert.ToInt32(file.Size)), file.Parameter ?? "image", file.Name);
        }
        string content2 = JsonConvert.SerializeObject(instance);
        content.Add(new StringContent(content2, Encoding.UTF8, "application/json"));

        return (InternalExceptionHandler(() => Client.PostAsync(url, content)));
    }

    protected Task<Response<T>> PostFile<T>(string url, params FileContent[] files) where T : class
    {
        MultipartFormDataContent content = new MultipartFormDataContent();
        content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");

        foreach (FileContent file in files)
        {
            content.Add(new StreamContent(file.Stream, Convert.ToInt32(file.Size)), file.Parameter ?? "image", file.Name);
        }

        return (InternalExceptionHandler<T>(() => Client.PostAsync(url, content)));
    }

    protected Task<Response<T>> PostFile<T, U>(string url, U instance, params FileContent[] files) where T : class
    {
        MultipartFormDataContent content1 = new MultipartFormDataContent();
        content1.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
        
        foreach (FileContent file in files)
        {
            content1.Add(new StreamContent(file.Stream, Convert.ToInt32(file.Size)), file.Parameter ?? "image", file.Name);
        }

        string content2 = JsonConvert.SerializeObject(instance);
        content1.Add(new StringContent(content2, Encoding.UTF8, "application/json"));
        return (InternalExceptionHandler<T>(() => Client.PostAsync(url, content1)));
    }
}