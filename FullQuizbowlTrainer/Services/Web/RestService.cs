using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace FullQuizbowlTrainer.Services.Web
{
    public interface IRestService
    {
        Task<string> Get(string uri);
    }

    public class RestService : IRestService
    {
        private const string BaseUrL = "https://power15-restful.herokuapp.com";

        HttpClient httpClient;

        public RestService()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(BaseUrL);
        }

        public async Task<string> Get(string uri)
        {
            var response = await httpClient.GetAsync(uri);
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}
