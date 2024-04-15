using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents.LayoutViewComponents
{
	public class _LayoutNavbarComponentPartial : ViewComponent
	{
		MyPortfolioContext context = new MyPortfolioContext(); // Database context
		public IViewComponentResult Invoke()
		{
			var values = context.ToDoLists.Where(x => x.Status == false).ToList(); // ToDoList values
			ViewBag.toDoListCount = context.ToDoLists.Where(x => x.Status == false).Count(); // ToDoList count
			return View(values);
		}
	}
}
