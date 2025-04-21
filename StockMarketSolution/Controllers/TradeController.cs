using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ServiceContracts;
using ServiceContracts.DTO;
using StockMarketSolution.Models;
using Rotativa.AspNetCore;

namespace StockMarketSolution.Controllers {
	[Route("[controller]")]
	public class TradeController(
		IOptions<TradingOptions> tradingOptions,
		IStocksService stocksService,
		IFinnhubService finnhubService,
		IConfiguration configuration) : Controller {
		private readonly TradingOptions _tradingOptions = tradingOptions.Value;

		[Route("[action]/{stockSymbol}")]
		[Route("~/[controller]/{stockSymbol}")]
		public async Task<IActionResult> Index(string? stockSymbol) {
			if (string.IsNullOrEmpty(stockSymbol))
				stockSymbol = "MSFT";

			var companyProfileDictionary = await
				finnhubService.GetCompanyProfile(stockSymbol);

			var stockQuoteDictionary =
				await finnhubService.GetStockPriceQuote(stockSymbol);

			var stockTrade = new StockTrade() { StockSymbol = stockSymbol };

			if (companyProfileDictionary != null && stockQuoteDictionary != null) {
				stockTrade = new StockTrade()
					{
						StockSymbol = companyProfileDictionary["ticker"].ToString(),
						StockName = companyProfileDictionary["name"].ToString(),
						Quantity = _tradingOptions.DefaultOrderQuantity ?? 0,
						Price = Convert.ToDouble(stockQuoteDictionary["c"].ToString())
					};
			}

			ViewBag.FinnhubToken = configuration["FinnhubToken"];
			return View(stockTrade);
		}

		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> BuyOrder(BuyOrderRequest buyOrderRequest) {
			buyOrderRequest.DateAndTimeOfOrder = DateTime.Now;
			ModelState.Clear();
			TryValidateModel(buyOrderRequest);

			if (!ModelState.IsValid) {
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				var stockTrade = new StockTrade()
					{
						StockName = buyOrderRequest.StockName, Quantity = buyOrderRequest.Quantity,
						StockSymbol = buyOrderRequest.StockSymbol
					};
				return View("Index", stockTrade);
			}

			await stocksService.CreateBuyOrder(buyOrderRequest);

			return RedirectToAction(nameof(Orders));
		}


		[Route("[action]")]
		[HttpPost]
		public async Task<IActionResult> SellOrder(SellOrderRequest sellOrderRequest) {
			sellOrderRequest.DateAndTimeOfOrder = DateTime.Now;

			ModelState.Clear();
			TryValidateModel(sellOrderRequest);

			if (!ModelState.IsValid) {
				ViewBag.Errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
				var stockTrade = new StockTrade()
					{
						StockName = sellOrderRequest.StockName, Quantity = sellOrderRequest.Quantity,
						StockSymbol = sellOrderRequest.StockSymbol
					};
				return View("Index", stockTrade);
			}

			await stocksService.CreateSellOrder(sellOrderRequest);

			return RedirectToAction(nameof(Orders));
		}


		[Route("[action]")]
		public async Task<IActionResult> Orders() {
			var buyOrderResponses = await stocksService.GetBuyOrders();
			var sellOrderResponses = await stocksService.GetSellOrders();

			var orders = new Orders() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };

			ViewBag.TradingOptions = _tradingOptions;

			return View(orders);
		}

		[Route("[action]")]
		[HttpGet]
		public async Task<IActionResult> OrdersPDF() {
			var buyOrderResponses = await stocksService.GetBuyOrders();
			var sellOrderResponses = await stocksService.GetSellOrders();

			var orders = new Orders() { BuyOrders = buyOrderResponses, SellOrders = sellOrderResponses };

			return new ViewAsPdf("OrdersPDF", orders, ViewData)
				{
					PageMargins = new Rotativa.AspNetCore.Options.Margins()
						{
							Top = 5, Right = 5, Bottom = 5, Left = 5
						},
					PageOrientation = Rotativa.AspNetCore.Options.Orientation.Portrait
				};
		}
	}
}