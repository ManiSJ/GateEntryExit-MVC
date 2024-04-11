using GateEntryExit_MVC.Models.Shared;
using Microsoft.AspNetCore.Mvc;

namespace GateEntryExit_MVC.ViewComponents
{
    public class PaginationViewComponent : ViewComponent
    {
        public PaginationViewComponent()
        {
            
        }

        public async Task<IViewComponentResult> InvokeAsync(PaginationModel model)
        {
            return View("~/ViewComponents/PaginationView.cshtml", model);
        }
    }
}
