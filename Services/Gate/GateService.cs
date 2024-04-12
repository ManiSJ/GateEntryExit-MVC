using GateEntryExit_MVC.Helpers;
using GateEntryExit_MVC.Models.Gate;
using GateEntryExit_MVC.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GateEntryExit_MVC.Services.Gate
{
    public class GateService : IGateService
    {
        private readonly IHttpClientService _httpClientService;

        public GateService(IHttpClientService httpClientService)
        {
            _httpClientService = httpClientService;
        }

        public async Task<GetAllGatesDto> GetAllAsync(int pageNumber = 1)
        {
            var maxResultCount = 5;
            var skipCount = (pageNumber - 1) * maxResultCount;
            var model = new GetAllGatesDto();
            var endpoint = ApiEndpoints.baseUrl + ApiEndpoints.gateGetAll;
            var postData = new GetAllDto { MaxResultCount = maxResultCount, SkipCount = skipCount, Sorting = "" };
            return await _httpClientService.GetAllAsync(model, postData, endpoint);
        }
    }
}
