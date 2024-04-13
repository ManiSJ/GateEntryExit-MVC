using GateEntryExit_MVC.Services.Gate;
using Microsoft.AspNetCore.Mvc;

namespace GateEntryExit_MVC.Controllers
{
    public class GateModalController : Controller
    { 
        private readonly IGateService _gateService;

        public GateModalController(IGateService gateService)
        {
            _gateService = gateService;
        }

        [HttpGet]
        public async Task<IActionResult> FetchGates(int pageNumber = 1, Guid? selectedGateValue = null)
        {
            var allGates = await _gateService.GetAllAsync(pageNumber);
            return View(new { gates = allGates, selectedGate = selectedGateValue });
        }
    }
}
