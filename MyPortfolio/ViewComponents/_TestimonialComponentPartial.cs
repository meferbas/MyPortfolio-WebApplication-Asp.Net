﻿using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.ViewComponents
{
    public class _TestimonialComponentPartial : ViewComponent
    {
       MyPortfolioContext context = new MyPortfolioContext();
        public IViewComponentResult Invoke()
         {
            var values = context.Testimonials.ToList();
            return View(values);
         }
    }
}
