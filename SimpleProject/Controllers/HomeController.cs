using Microsoft.AspNetCore.Mvc;

namespace SimpleProject.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
