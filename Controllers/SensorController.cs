using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.Sensor;
using GateEntryExit_MVC.Models.Shared;
using GateEntryExit_MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace GateEntryExit_MVC.Controllers
{
    public class SensorController : Controller
    {
        private readonly IHttpClientService _httpClientService;

        public SensorController(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<IActionResult> GetAll()
        {
            var model = new GetAllSensorsDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorGetAll;
            var postData = new GetAllDto { MaxResultCount = 5, SkipCount = 0, Sorting = "" };
            model = await _httpClientService.GetAllAsync(model, postData, endpoint);
            return View(model.Items);
        }

        public async Task<IActionResult> GetAllWithDetails()
        {
            var model = new GetAllSensorWithDetailsOutputDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorGetAllWithDetails;
            var postData = new GetAllSensorWithDetailsInputDto() { MaxResultCount = 5, 
                SkipCount = 0, 
                Sorting = "", 
                FromDate = null,
                ToDate = null,
                GateIds = new Guid[0]
            };
            model = await _httpClientService.GetAllAsync(model, postData, endpoint);
            return View(model.Items);
        }
    }
}
