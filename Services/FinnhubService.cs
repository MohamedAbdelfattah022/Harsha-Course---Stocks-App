using Microsoft.Extensions.Logging;
using RepositoryContracts;
using ServiceContracts;

namespace Services {
	public class FinnhubService(IFinnhubRepository finnhubRepository, ILogger<FinnhubService> logger) : IFinnhubService {
		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol) {
			logger.LogInformation("Getting company profile for symbol: {Symbol}", stockSymbol);
			var result = await finnhubRepository.GetCompanyProfile(stockSymbol);
			if (result is null)
				logger.LogWarning("No company profile found for symbol: {Symbol}", stockSymbol);
			return result;
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol) {
			logger.LogInformation("Getting stock price quote for symbol: {Symbol}", stockSymbol);
			return await finnhubRepository.GetStockPriceQuote(stockSymbol);
		}

		public async Task<List<Dictionary<string, string>>?> GetStocks() {
			return await finnhubRepository.GetStocks();
		}

		public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch) {
			return await finnhubRepository.SearchStocks(stockSymbolToSearch);
		}
	}
}