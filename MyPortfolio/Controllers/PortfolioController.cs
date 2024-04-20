using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize(Roles = "admin")]

    public class PortfolioController : Controller
	{
		private readonly MyPortfolioContext context;
		public PortfolioController(MyPortfolioContext _context)
		{
            context = _context;
        }
		public IActionResult Index()
		{
			var values = context.Portfolios.ToList();
			return View(values);
		}
		[HttpGet]
		public IActionResult CreatePortfolio()
		{
			return View();
		}
		[HttpPost]
		public IActionResult CreatePortfolio(Portfolio portfolio)
		{
			context.Portfolios.Add(portfolio);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult DeletePortfolio(int id)
		{
			var value = context.Portfolios.Find(id);
			context.Portfolios.Remove(value);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult UpdatePortfolio(int id)
		{
			var value = context.Portfolios.Find(id);
			return View(value);
		}
		[HttpPost]
		public IActionResult UpdatePortfolio(Portfolio portfolio)
		{
			context.Portfolios.Update(portfolio);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
