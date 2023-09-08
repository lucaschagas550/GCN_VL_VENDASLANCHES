using Microsoft.AspNetCore.Mvc;

namespace VL_VendasLanches.Components
{
    public class SummaryViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}