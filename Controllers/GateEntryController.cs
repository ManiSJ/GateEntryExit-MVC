using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.Gate;
using GateEntryExit_MVC.Models.GateEntry;
using GateEntryExit_MVC.Models.Sensor;
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
            var allGateEntry = await GetAllAsync(pageNumber);
            return View("~/Views/GateEntry/GetAll.cshtml", new GateEntryCrudWithList()
            {
                GateEntry = new GateEntryDto()
                {
                    Id = null,
                    GateId = null,
                    GateName = "",
                    NumberOfPeople = null,
                    Date = null,
                    Hour = null,
                    Minute = null,
                },
                GateEntries = allGateEntry,
                PageNumber = Request.Query["pageNumber"].FirstOrDefault() != null ? Convert.ToInt32(Request.Query["pageNumber"]) : 1
            });
        }

        private async Task<GetAllGateEntriesDto> GetAllAsync(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var model = new GetAllGateEntriesDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryGetAll;
            var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };
            return await _httpClientService.GetAllAsync(model, postData, endpoint);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, int pageNumber)
        {
            var gateEntry = new GateEntryDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryGetById.Replace("{id}", "") + id;
            gateEntry = await _httpClientService.GetAsync(gateEntry, endpoint);

            var allGateEntry = await GetAllAsync(pageNumber);
            return View("~/Views/GateEntry/GetAll.cshtml", new GateEntryCrudWithList()
            {
                GateEntry = new GateEntryDto()
                {
                    Id = gateEntry.Id,
                    GateId = gateEntry.GateId,
                    GateName = gateEntry.GateName,
                    NumberOfPeople = gateEntry.NumberOfPeople,
                    Date = gateEntry.TimeStamp.Date,
                    Hour = gateEntry.TimeStamp.Hour,
                    Minute = gateEntry.TimeStamp.Minute,
                },
                GateEntries = allGateEntry,
                PageNumber = Request.Query["pageNumber"].FirstOrDefault() != null ? Convert.ToInt32(Request.Query["pageNumber"]) : 1
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, GateEntryCrudWithList gateEntryCrudWithListModel)
        {
            var model = gateEntryCrudWithListModel.GateEntry;
            if (id == null)
            {              
                var dateTime = new DateTime(model.Date.Value.Year,
                    model.Date.Value.Month,
                    model.Date.Value.Day,
                    model.Hour.Value,
                    model.Minute.Value,
                    0);
                var postData = new CreateGateEntryDto() { 
                    GateId = model.GateId.Value, 
                    NumberOfPeople = model.NumberOfPeople.Value, 
                    TimeStamp = dateTime
                };
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryCreate;
                await _httpClientService.CreateAsync(model, postData, endpoint);
            }
            else
            {
                var dateTime = new DateTime(model.Date.Value.Year,
                    model.Date.Value.Month,
                    model.Date.Value.Day,
                    model.Hour.Value,
                    model.Minute.Value,
                    0);
                var postData = new UpdateGateEntryDto() { 
                    Id = model.Id.Value, 
                    NumberOfPeople = model.NumberOfPeople.Value, 
                    TimeStamp = dateTime
                };
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateEntryEdit;
                await _httpClientService.EditAsync(model, postData, endpoint);
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
