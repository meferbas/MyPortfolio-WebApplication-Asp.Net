using Microsoft.AspNetCore.Mvc;
using MyPortfolio.DAL.Context;

namespace MyPortfolio.ViewComponents
{
    public class _PortfolioComponentPartial : ViewComponent
    {
		MyPortfolioContext portfolioContext = new MyPortfolioContext();

		public IViewComponentResult Invoke()
        {
            var values = portfolioContext.Portfolios.ToList();
            return View(values);
        }
    }
}
