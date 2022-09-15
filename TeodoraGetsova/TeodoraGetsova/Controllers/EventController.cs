using Microsoft.AspNetCore.Mvc;

namespace TeodoraGetsova.Controllers
{
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
