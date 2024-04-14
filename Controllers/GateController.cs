using GateEntryExit_MVC.Models.Gate;
using GateEntryExit_MVC.Helpers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using GateEntryExit_MVC.Models.Shared;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Reflection;
using GateEntryExit_MVC.Services;
using GateEntryExit_MVC.Services.Gate;

namespace GateEntryExit_MVC.Controllers
{
    public class GateController : Controller
    {      
        private readonly ILogger<GateController> _logger;
        private readonly IHttpClientService _httpClientService;
        private readonly IGateService _gateService;

        public GateController(ILogger<GateController> logger,
            IHttpClientService httpClientService,
            IGateService gateService)
        {           
            _logger = logger;
            _httpClientService = httpClientService;
            _gateService = gateService;
        }

        public async Task<IActionResult> GetAll(int pageNumber = 1)
        {
            var model = await _gateService.GetAllAsync(pageNumber);
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(Guid? id)
        {
            if(id == null)
            {
                return View(new GateDto(){ Id = null });
            }
            else
            {
                var model = new GateDto();
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateGetById.Replace("{id}", "") + id;
                model = await _httpClientService.GetAsync(model, endpoint);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, GateDto model)
        {
            if (id == null)
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateCreate;
                await _httpClientService.CreateAsync(model, model, endpoint);
            }
            else
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEdit;
                await _httpClientService.EditAsync(model, model, endpoint);
            }
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateDelete.Replace("{id}", "") + id;
                await _httpClientService.DeleteAsync(endpoint);
            }
            return RedirectToAction("GetAll");
        }
    }
}
