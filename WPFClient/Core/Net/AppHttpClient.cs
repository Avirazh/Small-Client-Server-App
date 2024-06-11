using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WPFClient.Jwt;

namespace WPFClient.Net
{
    public class AppHttpClient : HttpClient
    {
        public AppHttpClient()
        {
            BaseAddress = new Uri("https://localhost:7275");
            TryGetToken();
        }
        public async Task<T> GetAsync<T>(string uri)
        {
            HttpResponseMessage response = await GetAsync(uri);

            string jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<T>(jsonResponse);
        }

        private void TryGetToken()
        {
            var token = JwtService.ReadToken();
            if (token != null)
            {
                DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", JwtService.ReadToken());
            }
        }
    }
}