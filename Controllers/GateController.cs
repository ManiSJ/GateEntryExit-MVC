using GateEntryExit_MVC.Models.Gate;
using GateEntryExit_MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GateEntryExit_MVC.Models.Shared;
using System.Text;
using Microsoft.Extensions.Logging;

namespace GateEntryExit_MVC.Controllers
{
    public class GateController : Controller
    {
        private readonly HttpClient _client;
        private readonly ILogger<GateController> _logger;
        public GateController(ILogger<GateController> logger)
        {
            _client = new HttpClient();
            _logger = logger;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new GetAllGatesDto();
            var postData = new GetAllDto { MaxResultCount = 5, SkipCount = 0, Sorting = "" };
            var jsonString = JsonConvert.SerializeObject(postData);
            var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateGetAll;
            HttpResponseMessage responseMessage = await _client.PostAsync(endpoint, content);

            if(responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                model = JsonConvert.DeserializeObject<GetAllGatesDto>(data);
            }
            return View(model.Items);
        }
    }
}
