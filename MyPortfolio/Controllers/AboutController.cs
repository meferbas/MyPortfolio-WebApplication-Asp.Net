using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
	public class AboutController : Controller
	{
		MyPortfolioContext context = new MyPortfolioContext();
		public IActionResult Index()
		{
			ViewBag.aboutTitle = context.Abouts.Select(x => x.Title).FirstOrDefault();
			ViewBag.aboutSubDescription = context.Abouts.Select(x => x.SubDescription).FirstOrDefault();
			ViewBag.aboutDetail = context.Abouts.Select(x => x.Details).FirstOrDefault();
			return View();
		}
		[HttpGet]
		public IActionResult UpdateAbout(int id)
		{
			var value = context.Abouts.Find(id);
			return View(value);
		}
		[HttpPost]
		public IActionResult UpdateAbout(About about)
		{
			context.Abouts.Update(about);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
