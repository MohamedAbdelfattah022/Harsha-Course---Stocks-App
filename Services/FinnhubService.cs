using RepositoryContracts;
using ServiceContracts;

namespace Services {
	public class FinnhubService(IFinnhubRepository finnhubRepository) : IFinnhubService {
		public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol) {
			return await finnhubRepository.GetCompanyProfile(stockSymbol);
		}

		public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol) {
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