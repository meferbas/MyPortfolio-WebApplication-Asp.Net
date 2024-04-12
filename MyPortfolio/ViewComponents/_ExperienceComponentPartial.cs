using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    
    public class _ExperienceComponentPartial : ViewComponent
    {
        MyPortfolioContext portfolioContext=new MyPortfolioContext();
        public IViewComponentResult Invoke()
        {
            var values= portfolioContext.Experiences.ToList();
            return View(values);
        }
    }
}
