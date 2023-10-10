using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheLair.HTTP.Json
{
    public partial class JsonHttpClient
    {
        private readonly HttpClient Client = new HttpClient();

        public JsonHttpClient() { }

        public JsonHttpClient(HttpClient client)
        {
            Client = client;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HttpClient GetInnerClient()
        {
            return Client;
        }

        public JsonHttpClient Configure(Action<HttpClient> action)
        {
            action(Client);

            return this;
        }

        public HttpContent PrepareContent(object obj)
        {
            string data = JsonConvert.SerializeObject(obj);

            return (new StringContent(data, Encoding.UTF8, "application/json"));
        }

        internal async Task<Response> InternalExceptionHandler(Func<Task<HttpResponseMessage>> act)
        {
            try
            {
                HttpResponseMessage result = await act();

                return (new Response
                {
                    Code = result.StatusCode
                });
            }
            catch (Exception e)
            {
                return (new Response(e));
            }
        }

        internal async Task<Response<T>> InternalExceptionHandler<T>(Func<Task<HttpResponseMessage>> act, Func<HttpResponseMessage, Task<T>>? resolver = null) where T : class
        {
            try
            {
                HttpResponseMessage result = await act();

                return (new Response<T>
                {
                    Code = result.StatusCode,
                    Content = (resolver == null 
                        ? JsonConvert.DeserializeObject<T>(await result.Content.ReadAsStringAsync())!
                        : await resolver(result))
                });
            }
            catch (Exception e)
            {
                return (new Response<T>(e));
            }
        }
    }
}
