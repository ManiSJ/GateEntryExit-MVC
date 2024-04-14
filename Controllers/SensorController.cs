using GateEntryExit.Dtos.Sensor;
using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.Gate;
using GateEntryExit_MVC.Models.GateExit;
using GateEntryExit_MVC.Models.Sensor;
using GateEntryExit_MVC.Models.Shared;
using GateEntryExit_MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
            var allSensors = await GetAllAsync(pageNumber);
            return View("~/Views/Sensor/GetAll.cshtml", new SensorCrudWithList()
            {
                SensorDetails = new SensorDetailsDto() { 
                    Id = null , 
                    GateDetails = new GateDetailsDto()
                    {
                        Id = null,
                        Name = ""
                    },
                    Name = ""
                },
                AllSensors = allSensors,
                PageNumber = Request.Query["pageNumber"].FirstOrDefault() != null ? Convert.ToInt32(Request.Query["pageNumber"]) : 1
            });
        }

        private async Task<GetAllSensorsDto> GetAllAsync(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var allSensors = new GetAllSensorsDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorGetAll;
            var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };
            return await _httpClientService.GetAllAsync(allSensors, postData, endpoint);
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
        public async Task<IActionResult> Edit(Guid id, int pageNumber)
        {
            var sensorDetail = new SensorDetailsDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorGetById.Replace("{id}", "") + id;
            sensorDetail = await _httpClientService.GetAsync(sensorDetail, endpoint);

            var allSensors = await GetAllAsync(pageNumber);
            return View("~/Views/Sensor/GetAll.cshtml", new SensorCrudWithList()
            {
                SensorDetails = sensorDetail,
                AllSensors = allSensors,
                PageNumber = Request.Query["pageNumber"].FirstOrDefault() != null ? Convert.ToInt32(Request.Query["pageNumber"]) : 1
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, SensorCrudWithList sensorCrudWithListModel)
        {
            var model = sensorCrudWithListModel.SensorDetails;
            if (id == null)
            {
                var postData = new CreateSensorDto() { Name = model.Name, GateId = model.GateDetails.Id.Value };
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorCreate;
                await _httpClientService.CreateAsync(model, postData, endpoint);
            }
            else
            {
                var postData = new UpdateSensorDto() { Name = model.Name, Id = model.Id.Value }; ;
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.sensorEdit;
                await _httpClientService.EditAsync(model, postData, endpoint);
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
