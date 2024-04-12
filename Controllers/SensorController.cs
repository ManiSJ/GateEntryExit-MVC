using GateEntryExit.Dtos.Sensor;
using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.GateExit;
using GateEntryExit_MVC.Models.Sensor;
using GateEntryExit_MVC.Models.Shared;
using GateEntryExit_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

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

        [HttpGet]
        public async Task<IActionResult> AddOrEdit(Guid? id)
        {
            if (id == null)
            {
                return View(new SensorDetailsDto() { Id = null });
            }
            else
            {
                var model = new SensorDetailsDto();
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorGetById.Replace("{id}", "") + id;
                model = await _httpClientService.GetAsync(model, endpoint);
                return View(model);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, SensorDetailsDto model)
        {
            if (id == null)
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorCreate;
                await _httpClientService.CreateAsync(model, model, endpoint);
            }
            else
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorEdit;
                await _httpClientService.EditAsync(model, model, endpoint);
            }
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorDelete.Replace("{id}", "") + id;
                await _httpClientService.DeleteAsync(endpoint);
            }
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> EmailReport()
        {
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorExcelReport;
            var postData = new GetAllSensorWithDetailsReportInputDto()
            {
                FromDate = null,
                ToDate = null,
                GateIds = new Guid[0]
            };
            await _httpClientService.PostAsync(postData, endpoint);
            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> PdfReport()
        {
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorPdfReport;
            var postData = new GetAllSensorWithDetailsReportInputDto()
            {
                FromDate = null,
                ToDate = null,
                GateIds = new Guid[0]
            };
            await _httpClientService.PostAsync(postData, endpoint);
            return RedirectToAction("GetAllWithDetails");
        }
    }
}
