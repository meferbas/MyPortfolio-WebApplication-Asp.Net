using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.Controllers
{
    [Authorize(Roles = "admin")]

    public class StatisticController : Controller
	{
		private readonly MyPortfolioContext context;
		public StatisticController(MyPortfolioContext _context)
		{
            context = _context;
        }
		public IActionResult Index()
		{
			ViewBag.v1= context.Skills.Count();
			ViewBag.v2 = context.Messages.Count();
			ViewBag.v3 = context.Messages.Where(x => x.isRead == false).Count();	
			ViewBag.v4 = context.Messages.Where(x => x.isRead == true).Count();	
			return View();
		}
	}
}
