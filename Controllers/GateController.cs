using GateEntryExit_MVC.Models.Gate;
using GateEntryExit_MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GateEntryExit_MVC.Models.Shared;
using System.Text;
using Microsoft.Extensions.Logging;
using GateEntryExit_MVC.Services;

namespace GateEntryExit_MVC.Controllers
{
    public class GateController : Controller
    {      
        private readonly ILogger<GateController> _logger;
        private readonly IHttpClientService _httpClientService;

        public GateController(ILogger<GateController> logger,
            IHttpClientService httpClientService)
        {           
            _logger = logger;
            _httpClientService = httpClientService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new GetAllGatesDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateGetAll;
            var postData = new GetAllDto { MaxResultCount = 5, SkipCount = 0, Sorting = "" };
            model = await _httpClientService.GetAllAsync(model, postData, endpoint);
            return View(model.Items);
        }
    }
}
