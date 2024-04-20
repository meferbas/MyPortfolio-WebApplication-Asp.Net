using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
    [Authorize(Roles = "admin")]

    public class ToDoListController : Controller
	{
		private readonly MyPortfolioContext context;
		public ToDoListController(MyPortfolioContext context)
		{
            this.context = context;
        }
		public IActionResult Index()
		{
			var values = context.ToDoLists.ToList();
			return View(values);
		}
		[HttpGet]
		public IActionResult CreateToDoList()
		{
			return View();
		}
		[HttpPost]
		public IActionResult CreateToDoList(ToDoList toDoList)
		{
			toDoList.Status = false;
			context.ToDoLists.Add(toDoList);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult DeleteToDoList(int id)
		{
			var value = context.ToDoLists.Find(id);
			context.ToDoLists.Remove(value);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult UpdateToDoList(int id)
		{
			var value = context.ToDoLists.Find(id);
			return View(value);
		}
		[HttpPost]
		public IActionResult UpdateToDoList(ToDoList toDoList)
		{
			context.ToDoLists.Update(toDoList);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult ToDoListStatus(int id)
		{
			var value = context.ToDoLists.Find(id);
			value.Status = !value.Status;
			context.ToDoLists.Update(value);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}
