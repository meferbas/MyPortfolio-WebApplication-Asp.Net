﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.Controllers
{
	public class MessageController : Controller
	{
		MyPortfolioContext context = new MyPortfolioContext();
		public IActionResult Inbox()
		{
			var values = context.Messages.ToList();
			return View(values);
		}

        public IActionResult SendMessage()
		{
			return View("~/Views/Shared/Components/_ContactComponentPartial/Default.cshtml");
		}
        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (ModelState.IsValid)
            {
                message.SendDate = DateTime.Now;
                message.isRead = false;
                context.Messages.Add(message);
                context.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }



        public IActionResult ChangeIsReadToTrue(int id)
		{
			var message = context.Messages.Find(id);
			message.isRead = true;
			context.SaveChanges();
			return RedirectToAction("Inbox");
		}
		public IActionResult ChangeIsReadToFalse(int id)
		{
			var message = context.Messages.Find(id);
			message.isRead = false;
			context.SaveChanges();
			return RedirectToAction("Inbox");
		}
		public IActionResult DeleteMessage(int id)
		{
			var message = context.Messages.Find(id);
			context.Messages.Remove(message);
			context.SaveChanges();
			return RedirectToAction("Inbox");
		}
		public IActionResult MessageDetails(int id)
		{
			var message = context.Messages.Find(id);
			return View(message);
		}
	}
}
