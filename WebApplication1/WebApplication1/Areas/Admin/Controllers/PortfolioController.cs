using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PortfolioController : Controller
    {
        AppDbContext _db;

        public PortfolioController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            List<Portfolio> portfolios = _db.portfolios.ToList();
            return View(portfolios);
        }
        public IActionResult Delete(int? id)
        {
            var portfolio = _db.portfolios.First(x => x.Id == id);
            _db.portfolios.Remove(portfolio);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Portfolio portfolio)
        {
            string path = @"C:\Users\II novbe\Desktop\Finishtoback\WebApplication1\WebApplication1\wwwroot\upload\portfolio\";
            string filename = portfolio.Photofile.FileName;
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))

            {

                portfolio.Photofile.CopyTo(stream);

            }
            portfolio.ImageUrl = filename;

            if (!ModelState.IsValid)
            {
                return View();
            }
            var portfolioId = _db.portfolios.Add(portfolio);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
        public IActionResult Update(int id)
        {
            var portfolio = _db.portfolios.FirstOrDefault(x => x.Id == id);
            if (portfolio==null)
            {
                return RedirectToAction("Index");
            }
            return View(portfolio);
        }
        [HttpPost]
        public  IActionResult Update(Portfolio newPortfolio) 
        {
            var oldPortfolio = _db.portfolios.FirstOrDefault(x => x.Id == newPortfolio.Id);
            string path = @"C:\Users\II novbe\Desktop\Finishtoback\WebApplication1\WebApplication1\wwwroot\upload\portfolio\";
            string filename = newPortfolio.Photofile.FileName;
            using (FileStream stream = new FileStream(path + filename, FileMode.Create))

            {

                newPortfolio.Photofile.CopyTo(stream);

            }
            oldPortfolio.ImageUrl = filename;
            
            if (oldPortfolio==null) { return RedirectToAction("Index"); }

            if (!ModelState.IsValid)
            {
                return View();
            }
            oldPortfolio.Name = newPortfolio.Name;
            oldPortfolio.Description = newPortfolio.Description;
            oldPortfolio.Photofile = newPortfolio.Photofile;
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
