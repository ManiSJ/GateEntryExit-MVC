using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.Gate;
using GateEntryExit_MVC.Models.Shared;
using Newtonsoft.Json;
using System.Reflection;
using System.Text;

namespace GateEntryExit_MVC.Services
{
    public class HttpClientService : IHttpClientService
    {
        private readonly HttpClient _client;

        public HttpClientService()
        {
            _client = new HttpClient();
        }

        public async Task<T> GetAllAsync<T>(T model, object postData, string endpoint)
        {
            return await ProcessRequestAsync(model, postData, endpoint);
        }

        public async Task PostAsync(object postData, string endpoint)
        {
            var jsonString = JsonConvert.SerializeObject(postData);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(endpoint, content);
        }

        private async Task<T> ProcessRequestAsync<T>(T model, object postData, string endpoint)
        {
            var jsonString = JsonConvert.SerializeObject(postData);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage = await _client.PostAsync(endpoint, content);

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<T>(data);
            }

            return model;
        }

        public async Task<T> CreateAsync<T>(T model, object postData, string endpoint)
        {
            return await ProcessRequestAsync(model, postData, endpoint);
        }

        public async Task<T> EditAsync<T>(T model, object postData, string endpoint)
        {
            return await ProcessRequestAsync(model, postData, endpoint);
        }

        public async Task DeleteAsync(string endpoint)
        {
            HttpResponseMessage responseMessage = await _client.DeleteAsync(endpoint);
        }

        public async Task<T> GetAsync<T>(T model, string endpoint)
        {
            HttpResponseMessage responseMessage = await _client.PostAsync(endpoint, null);

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<T>(data);
            }

            return model;
        }
    }
}
