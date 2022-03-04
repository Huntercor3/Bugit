using Microsoft.AspNetCore.Mvc;

namespace aspnetserver.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Authenticate()
        {
            return RedirectToAction("Index");
        }
    }
}