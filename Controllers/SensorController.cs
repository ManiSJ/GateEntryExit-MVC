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

        public async Task<IActionResult> GetAll(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var model = new GetAllSensorsDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorGetAll;
            var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };
            model = await _httpClientService.GetAllAsync(model, postData, endpoint);
            return View(model);
        }

        public async Task<IActionResult> GetAllWithDetails(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var model = new GetAllSensorWithDetailsOutputDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorGetAllWithDetails;
            var postData = new GetAllSensorWithDetailsInputDto() { 
                MaxResultCount = maxResultCount, 
                SkipCount = skipCount, 
                Sorting = "", 
                FromDate = null,
                ToDate = null,
                GateIds = new Guid[0]
            };
            model = await _httpClientService.GetAllAsync(model, postData, endpoint);
            return View(model);
        }
    }
}
