using Microsoft.AspNetCore.Mvc;

namespace VL_VendasLanches.Controllers
{
    public class ContatoController : Controller
    {
        [AutoValidateAntiforgeryToken]
        public IActionResult Index()
        {
            return View();
        }
    }
}
