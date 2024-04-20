using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    
    public class _ExperienceComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext portfolioContext;
        public _ExperienceComponentPartial(MyPortfolioContext _portfolioContext)
        {
            portfolioContext = _portfolioContext;
        }
        public IViewComponentResult Invoke()
        {
            var values= portfolioContext.Experiences.ToList();
            return View(values);
        }
    }
}
