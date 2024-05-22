using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext _context;

        public HomeController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<Portfolio> portfolios = _context.portfolios.ToList();
            return View(portfolios);
            
        }

       
    }
}
