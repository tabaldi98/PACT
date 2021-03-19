using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace app.Tabaldi.PACT.Infra.Data.HttpClient
{
    public interface IHttpClientBase
    {
        Task<HttpResponseMessage> GetAsync(string url, bool throwException = true);
        Task<HttpResponseMessage> PostAsync<T>(string url, T command);
    }

    public class HttpClientBase : IHttpClientBase
    {
        private readonly string _baseAddress;
        private readonly string _token;

        public HttpClientBase()
        {
            _baseAddress = System.Configuration.ConfigurationManager.AppSettings["ServerBaseAddress"];
            _token = System.Configuration.ConfigurationManager.AppSettings["Token"];
        }

        public async Task<HttpResponseMessage> GetAsync(string url, bool throwException = true)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseAddress);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await httpClient.GetAsync(url);

                if (throwException && !response.IsSuccessStatusCode) { throw new Exception(await response.Content.ReadAsStringAsync()); }

                return response;
            }
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string url, T command)
        {
            using (var httpClient = new System.Net.Http.HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseAddress);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _token);

                var response = await httpClient.PostAsJsonAsync(url, command);

                if (!response.IsSuccessStatusCode) { throw new Exception(await response.Content.ReadAsStringAsync()); }

                return response;
            }
        }
    }
}
