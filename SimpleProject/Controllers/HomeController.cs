using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleProject.Data;
using System.Threading.Tasks;

namespace SimpleProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        public HomeController( ApplicationDbContext context )
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var Categories =await _context.Category.Include(x=> x.Products.Take(5)).ToListAsync();
            return View(Categories);
        }
    }
}
