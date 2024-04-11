using Microsoft.AspNetCore.Mvc;

namespace GateEntryExit_MVC.Controllers
{
    public class GateEntryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
