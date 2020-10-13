using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FullQuizbowlTrainer.Models;
using Newtonsoft.Json;

namespace FullQuizbowlTrainer.Services.Web
{
    public interface IRestService
    {
        Task<string> Get(string uri);
        Task<string> Post(string uri, AnsweredRest ans);
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

        public async Task<string> Post(string uri, AnsweredRest ans)
        {
            string json = JsonConvert.SerializeObject(ans, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, content);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
