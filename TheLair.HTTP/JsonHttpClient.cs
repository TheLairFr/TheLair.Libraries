using System;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheLair.HTTP
{
    public class JsonHttpClient
    {
        private readonly HttpClient Client = new HttpClient();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public HttpClient GetInnerClient()
        {
            return (Client);
        }

        public JsonHttpClient Configure(Action<HttpClient> action)
        {
            action(Client);
            return (this);
        }

        public async Task<string> GetAsString(string url)
        {
            string result = await Client.GetStringAsync(url) ?? throw new Exception("ERROR");

            return (result);
        }

        public async Task<T> Get<T>(string url)
        {
            string result = await Client.GetStringAsync(url) ?? throw new Exception("ERROR");

            return (JsonConvert.DeserializeObject<T>(result));
        }

        public async Task<HttpResponseMessage> Post<T>(string url, T instance)
        {
            string data = JsonConvert.SerializeObject(instance);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await Client.PostAsync(url, content);

            return (result);
        }

        public async Task<T> Post<T, U>(string url, U instance)
        {
            string data = JsonConvert.SerializeObject(instance);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await Client.PostAsync(url, content);
            string r = await result.Content.ReadAsStringAsync();

            return (JsonConvert.DeserializeObject<T>(r));
        }

        public async Task<HttpResponseMessage> Put<T>(string url, T instance)
        {
            string data = JsonConvert.SerializeObject(instance);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await Client.PutAsync(url, content);

            return (result);
        }

        public async Task<T> Put<T, U>(string url, U instance)
        {
            string data = JsonConvert.SerializeObject(instance);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await Client.PutAsync(url, content);
            string r = await result.Content.ReadAsStringAsync();

            return (JsonConvert.DeserializeObject<T>(r));
        }

        public async Task<HttpResponseMessage> Patch<T>(string url, T instance)
        {
            string data = JsonConvert.SerializeObject(instance);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await Client.PatchAsync(url, content);

            return (result);
        }

        public async Task<T> Patch<T, U>(string url, U instance)
        {
            string data = JsonConvert.SerializeObject(instance);
            HttpContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage result = await Client.PatchAsync(url, content);
            string r = await result.Content.ReadAsStringAsync();

            return (JsonConvert.DeserializeObject<T>(r));
        }

        public async Task<HttpResponseMessage> Delete(string url)
        {
            HttpResponseMessage result = await Client.DeleteAsync(url);

            return (result);
        }

        public async Task<T> Delete<T>(string url)
        {
            HttpResponseMessage result = await Client.DeleteAsync(url);
            string r = await result.Content.ReadAsStringAsync();

            return (JsonConvert.DeserializeObject<T>(r));
        }
    }
}
