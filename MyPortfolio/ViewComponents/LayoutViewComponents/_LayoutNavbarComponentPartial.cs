using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents.LayoutViewComponents
{
	public class _LayoutNavbarComponentPartial : ViewComponent
	{
		private readonly MyPortfolioContext context;

		public _LayoutNavbarComponentPartial(MyPortfolioContext _context)
		{
            context = _context;
        }
		public IViewComponentResult Invoke()
		{
			var values = context.ToDoLists.Where(x => x.Status == false).ToList(); // ToDoList values
			ViewBag.toDoListCount = context.ToDoLists.Where(x => x.Status == false).Count(); // ToDoList count
			return View(values);
		}
	}
}
