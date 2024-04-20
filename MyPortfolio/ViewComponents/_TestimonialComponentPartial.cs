using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;
using MyPortfolio.DAL.Entities;

namespace MyPortfolio.ViewComponents
{
    public class _TestimonialComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext context;

        public _TestimonialComponentPartial(MyPortfolioContext context)
        {
            this.context = context;
        }
        public IViewComponentResult Invoke()
         {
            var values = context.Testimonials.ToList();
            return View(values);
         }
    }
}
