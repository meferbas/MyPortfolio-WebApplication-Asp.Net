using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _SkillComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext portfolioContext;

        public _SkillComponentPartial (MyPortfolioContext context)
        {
            portfolioContext = context;
        }
        public IViewComponentResult Invoke ()
        {
            var values = portfolioContext.Skills.ToList();
            return View(values);
        }
    }
}
