using GateEntryExit_MVC.Services.Gate;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GateEntryExit_MVC.Controllers
{
    public class GateModalController : Controller
    { 
        private readonly IGateService _gateService;

        public GateModalController(IGateService gateService)
        {
            _gateService = gateService;
        }

        [HttpPost]
        public async Task<IActionResult> FetchGates(int pageNumber = 1,
            bool isSingleSelection = true,  
            Guid? selectedGateValue = null,
            [FromBody] Guid[] selectedGateValues = null)
        {
            var allGates = await _gateService.GetAllAsync(pageNumber);
            Guid[] gateIds = new Guid[0];

            if (selectedGateValues != null)
            {
                gateIds = selectedGateValues;
            }

            return View(new { 
                gates = allGates, 
                selectedGate = selectedGateValue,
                selectedGates = gateIds,
                isSingleSelect = isSingleSelection 
            });
        }
    }
}
