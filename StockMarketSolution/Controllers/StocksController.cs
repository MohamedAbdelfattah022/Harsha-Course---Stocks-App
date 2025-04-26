using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using StockMarketSolution.Models;

namespace StockMarketSolution.Controllers;

[Route("[controller]")]
public class StocksController(IFinnhubService finnhubService, IOptions<TradingOptions> tradingOptions, ILogger<StocksController> logger)
	: Controller {
	private readonly TradingOptions _tradingOptions = tradingOptions.Value;

	[Route("/")]
	[Route("[action]/{stock?}")]
	[HttpGet]
	public async Task<IActionResult> Explore(string stock) {
		logger.LogInformation("Explore action called with stock: {Stock}", stock);

		if (_tradingOptions.Top25PopularStocks is null) {
			logger.LogError("Top25PopularStocks configuration is null");
			return NotFound();
		}

		var top25 = _tradingOptions.Top25PopularStocks.Split(',');

		var stocksDict = await finnhubService.GetStocks();
		if (stocksDict is null) {
			logger.LogError("Failed to retrieve stocks from Finnhub service");
			return NotFound();
		}

		var stocks = stocksDict
			.Where(temp => top25.Contains(Convert.ToString(temp["symbol"])))
			.Select(temp => new Stock()
				{
					StockName = Convert.ToString(temp["description"]), StockSymbol = Convert.ToString(temp["symbol"])
				})
			.ToList();


		// List<Stock> stocks = new()
		// 	{
		// 		new Stock()
		// 				{ StockName = "Microsoft", StockSymbol = "MSFT" },
		// 		new Stock()
		// 				{ StockName = "Apple", StockSymbol = "AAPL" },
		// 		new Stock()
		// 				{ StockName = "Google", StockSymbol = "GOOGL" },
		// 	};

		ViewBag.stock = stock;
		return View(stocks);
	}
}