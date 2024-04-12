using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.GateEntry;
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

        public async Task<IActionResult> GetAll(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var model = new GetAllGateExitsDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitGetAll;
            var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };
            model = await _httpClientService.GetAllAsync(model, postData, endpoint);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(Guid? id)
        {
            if (id == null)
            {
                return View(new GateExitDto() { Id = null });
            }
            else
            {
                var model = new GateExitDto();
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitGetById.Replace("{id}", "") + id;
                model = await _httpClientService.GetAsync(model, endpoint);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, GateExitDto model)
        {
            if (id == null)
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitCreate;
                await _httpClientService.CreateAsync(model, model, endpoint);
            }
            else
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitEdit;
                await _httpClientService.EditAsync(model, model, endpoint);
            }
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitDelete.Replace("{id}", "") + id;
                await _httpClientService.DeleteAsync(endpoint);
            }
            return RedirectToAction("GetAll");
        }
    }
}
