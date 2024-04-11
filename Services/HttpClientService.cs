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
    }
}
