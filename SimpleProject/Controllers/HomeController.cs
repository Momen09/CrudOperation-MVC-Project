using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using System.Threading.Tasks;

namespace SimpleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private string userName = "momen";

        public HomeController( ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            //Response.Cookies.Append("userName", userName, new Microsoft.AspNetCore.Http.CookieOptions
            //{
            //    Expires = DateTimeOffset.Now.AddDays(7)
            //});
            //HttpContext.Session.SetString("userName", userName);
            //HttpContext.Session.SetInt32("userId", 25);

            //TempData["userName"] = "userName";
            //TempData["userId"] = 25; 
            var Categories =await _context.Category.Include(x=> x.Products.Take(5)).ToListAsync();
            return View(Categories);
        }
        public IActionResult Privacy()
        { 
        return View();
        }
        [HttpPost]
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
            );

            return LocalRedirect(returnUrl);
        }
    }
}
