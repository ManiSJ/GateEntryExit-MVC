using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.Gate;
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

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(Guid? id)
        {
            if (id == null) 
            {
                return View(new GateEntryDto() { Id = null });
            } 
            else
            {
                var model = new GateEntryDto();
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryGetById.Replace("{id}", "") + id;
                model = await _httpClientService.GetAsync(model, endpoint);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, GateEntryDto model)
        {
            if (id == null)
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryCreate;
                await _httpClientService.CreateAsync(model, model, endpoint);
            }
            else
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryEdit;
                await _httpClientService.EditAsync(model, model, endpoint);
            }
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryDelete.Replace("{id}", "") + id;
                await _httpClientService.DeleteAsync(endpoint);
            }
            return RedirectToAction("GetAll");
        }
    }
}
