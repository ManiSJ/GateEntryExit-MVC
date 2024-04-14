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
            var allGateExit = await GetAllAsync(pageNumber);
            return View("~/Views/GateExit/GetAll.cshtml", new GateExitCrudWithList()
            {
                GateExit = new GateExitDto()
                {
                    Id = null,
                    GateId = null,
                    GateName = "",
                    NumberOfPeople = null,
                    Date = null,
                    Hour = null,
                    Minute = null,
                },
                GateExits = allGateExit,
                PageNumber = Request.Query["pageNumber"].FirstOrDefault() != null ? Convert.ToInt32(Request.Query["pageNumber"]) : 1
            });
        }

        private async Task<GetAllGateExitsDto> GetAllAsync(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var model = new GetAllGateExitsDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitGetAll;
            var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };
            return await _httpClientService.GetAllAsync(model, postData, endpoint);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id, int pageNumber)
        {
            var gateExit = new GateExitDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitGetById.Replace("{id}", "") + id;
            gateExit = await _httpClientService.GetAsync(gateExit, endpoint);

            var allGateExit = await GetAllAsync(pageNumber);
            return View("~/Views/GateExit/GetAll.cshtml", new GateExitCrudWithList()
            {
                GateExit = new GateExitDto()
                {
                    Id = gateExit.Id,
                    GateId = gateExit.GateId,
                    GateName = gateExit.GateName,
                    NumberOfPeople = gateExit.NumberOfPeople,
                    Date = gateExit.TimeStamp.Date,
                    Hour = gateExit.TimeStamp.Hour,
                    Minute = gateExit.TimeStamp.Minute,
                },
                GateExits = allGateExit,
                PageNumber = Request.Query["pageNumber"].FirstOrDefault() != null ? Convert.ToInt32(Request.Query["pageNumber"]) : 1
            });
        }

        [HttpPost]
        public async Task<IActionResult> AddOrEdit(Guid? id, GateExitCrudWithList gateExitCrudWithListModel)
        {
            var model = gateExitCrudWithListModel.GateExit;
            if (id == null)
            {
                var dateTime = new DateTime(model.Date.Value.Year,
                    model.Date.Value.Month,
                    model.Date.Value.Day,
                    model.Hour.Value,
                    model.Minute.Value,
                    0);
                var postData = new CreateGateExitDto()
                {
                    GateId = model.GateId.Value,
                    NumberOfPeople = model.NumberOfPeople.Value,
                    TimeStamp = dateTime
                };
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitCreate;
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
                var postData = new UpdateGateExitDto()
                {
                    Id = model.Id.Value,
                    NumberOfPeople = model.NumberOfPeople.Value,
                    TimeStamp = dateTime
                };
                var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateExitEdit;
                await _httpClientService.EditAsync(model, postData, endpoint);
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
