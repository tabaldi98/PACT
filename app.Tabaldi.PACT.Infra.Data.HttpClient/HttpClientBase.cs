using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient
{
    public interface IHttpClientBase
    {
        Task<HttpResponseMessage> GetAsync(string url);
        Task<HttpResponseMessage> PostAsync<T>(string url, T command);
    }

    public class HttpClientBase : IHttpClientBase
    {
        private readonly string _baseAddress;

        public HttpClientBase()
        {
            _baseAddress = System.Configuration.ConfigurationManager.AppSettings["ServerBaseAddress"];
        }

        public async Task<HttpResponseMessage> GetAsync(string url)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseAddress);

                var response = await httpClient.GetAsync(url);

                if (!response.IsSuccessStatusCode) { 
                    var a = await response.Content.ReadAsStringAsync();
                    throw new Exception(await response.Content.ReadAsStringAsync()); }

                return response;
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T command)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseAddress);

                var response = await httpClient.PostAsJsonAsync(url, command);

                if (!response.IsSuccessStatusCode) { throw new Exception(await response.Content.ReadAsStringAsync()); }

                return response;
            }
        }
    }
}
