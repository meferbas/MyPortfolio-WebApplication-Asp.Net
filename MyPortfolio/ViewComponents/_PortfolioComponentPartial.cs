using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _PortfolioComponentPartial : ViewComponent
    {
        private readonly MyPortfolioContext portfolioContext;

        public _PortfolioComponentPartial(MyPortfolioContext context)
        {
            portfolioContext = context;
        }

		public IViewComponentResult Invoke()
        {
            var values = portfolioContext.Portfolios.ToList();
            return View(values);
        }
    }
}
