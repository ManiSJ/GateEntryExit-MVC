using Microsoft.AspNetCore.Mvc;

namespace GateEntryExit_MVC.Controllers
{
    public class SensorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
