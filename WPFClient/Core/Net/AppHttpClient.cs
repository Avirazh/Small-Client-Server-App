using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace WPFClient.Net
{
    public class AppHttpClient : HttpClient
    {
        public AppHttpClient()
        {
            BaseAddress = new Uri("https://localhost:7275");
        }
        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync(uri);

            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }
    }
}