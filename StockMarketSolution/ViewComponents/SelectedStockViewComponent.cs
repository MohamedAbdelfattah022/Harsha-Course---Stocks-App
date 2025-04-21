using Microsoft.AspNetCore.Mvc;
using ServiceContracts;

namespace StockMarketSolution.ViewComponents;

public class SelectedStockViewComponent(IFinnhubService finnhubService) : ViewComponent {
	public async Task<IViewComponentResult> InvokeAsync(string? stockSymbol) {
		Dictionary<string, object>? companyProfileDict = null;

		if (stockSymbol != null) {
			companyProfileDict = await finnhubService.GetCompanyProfile(stockSymbol);
			var stockPriceDict = await finnhubService.GetStockPriceQuote(stockSymbol);
			if (stockPriceDict != null && companyProfileDict != null) {
				companyProfileDict.Add("price", stockPriceDict["c"]);
			}
		}

		if (companyProfileDict != null && companyProfileDict.ContainsKey("logo"))
			return View(companyProfileDict);
		
		return Content("");
	}
}