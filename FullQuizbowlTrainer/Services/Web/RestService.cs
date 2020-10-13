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
        Task<string> PostAnswer(string uri, AnsweredRest ans);
        Task<int> GetLogin(string uri, Login login);
        Task<bool> Report(Report report);
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

        public async Task<int> GetLogin(string uri, Login login)
        {
            string json = JsonConvert.SerializeObject(login, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(uri, content);
            string res =  await response.Content.ReadAsStringAsync();
            if (res.Equals("Username and password match")) return 0;
            else if (res.Equals("Incorrect password")) return 1;
            else return 2;
        }

        public async Task<string> PostAnswer(string uri, AnsweredRest ans)
        {
            string json = JsonConvert.SerializeObject(ans, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(uri, content);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<bool> Report(Report report)
        {
            string json = JsonConvert.SerializeObject(report, Formatting.Indented);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync("/report", content);
            string res = await response.Content.ReadAsStringAsync();

            if (res.Contains("success")) return true;
            else return false;
        }
    }
}
