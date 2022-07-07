using Newtonsoft.Json;
using Parks.Web.Repository.Abstractions;
using Parks.Web.Static;
using System.Text;
using System.Net;

namespace Parks.Web.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private static readonly string _aplicationJson = "application/json";

        public Repository(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<T?> GetByIdAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<T>(json);
            }

            return null;
        }

        public async Task<IEnumerable<T>?> GetAllAsync(string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<IEnumerable<T>>(json);
            }

            return null;
        }
        public async Task<ResponseCodes> CreateAsync(string url, T entity)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if(entity != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(entity),
                    Encoding.UTF8, 
                    _aplicationJson
                );
            }
            else
            {
                return ResponseCodes.Error;
            }

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);
            if(response.IsSuccessStatusCode)
            {
                return ResponseCodes.Success;
            }
            return ResponseCodes.Unfinished;
        }

        public async Task<ResponseCodes> DeleteAsync(string url, int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{url}{id}");
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                return ResponseCodes.Success;
            }

            return ResponseCodes.Error;
        }

        public async Task<ResponseCodes> UpdateAsync(string url, T entity)
        {
            var request = new HttpRequestMessage(HttpMethod.Patch, url);

            if(entity != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(entity), 
                    encoding: Encoding.UTF8,
                    _aplicationJson
                );
            }
            else
            {
                return ResponseCodes.Error;
            }
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                return ResponseCodes.Success;
            }

            return ResponseCodes.Unfinished;
        }
    }
}
