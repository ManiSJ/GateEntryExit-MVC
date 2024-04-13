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
            var allGates = await _gateService.GetAllAsync(pageNumber);
            return View("~/Views/Gate/GetAll.cshtml", new GateCrudWithList()
            {
                Gate = new GateDto() { Id = null },
                AllGates = allGates,
                PageNumber = Request.Query["pageNumber"].FirstOrDefault() != null ? Convert.ToInt32(Request.Query["pageNumber"]) : 1
            });
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, int pageNumber)
        {            
            var gate = new GateDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateGetById.Replace("{id}", "") + id;
            gate = await _httpClientService.GetAsync(gate, endpoint);

            var allGates = await _gateService.GetAllAsync(pageNumber);
            return View("~/Views/Gate/GetAll.cshtml", new GateCrudWithList()
            {
                Gate = gate,
                AllGates = allGates,
                PageNumber = Request.Query["pageNumber"].FirstOrDefault() != null ? Convert.ToInt32(Request.Query["pageNumber"]) : 1
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, GateCrudWithList gateCrudWithListModel)
        {
            var model = gateCrudWithListModel.Gate;
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
