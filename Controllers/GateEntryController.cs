using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.GateEntry;
using GateEntryExit_MVC.Models.Shared;
using GateEntryExit_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GateEntryExit_MVC.Controllers
{
    public class GateEntryController : Controller
    {
        private readonly IHttpClientService _httpClientService;
        public GateEntryController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<IActionResult> GetAll(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var model = new GetAllGateEntriesDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryGetAll;
            var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };
            model = await _httpClientService.GetAllAsync(model, postData, endpoint);
            return View(model);
        }
    }
}
