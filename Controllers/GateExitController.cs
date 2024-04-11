using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.GateExit;
using GateEntryExit_MVC.Models.Shared;
using GateEntryExit_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GateEntryExit_MVC.Controllers
{
    public class GateExitController : Controller
    {
        private readonly IHttpClientService _httpClientService;

        public GateExitController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new GetAllGateExitsDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitGetAll;
            var postData = new GetAllDto { MaxResultCount = 5, SkipCount = 0, Sorting = "" };
            model = await _httpClientService.GetAllAsync(model, postData, endpoint);
            return View(model.Items);
        }
    }
}
