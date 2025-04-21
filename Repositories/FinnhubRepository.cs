using System.Text.Json;
using Microsoft.Extensions.Configuration;
using RepositoryContracts;

namespace Repositories;

public class FinnhubRepository(IConfiguration configuration, IHttpClientFactory httpClientFactory) : IFinnhubRepository {
	public async Task<Dictionary<string, object>?> GetCompanyProfile(string stockSymbol) {
		HttpClient httpClient = httpClientFactory.CreateClient();

		string url = $"https://finnhub.io/api/v1/stock/profile2?symbol={stockSymbol}&token={configuration["FinnhubToken"]}";

		string responseBody = await httpClient.GetStringAsync(url);

		Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

		if (responseDictionary == null)
			throw new InvalidOperationException("No response from server");

		if (responseDictionary.ContainsKey("error"))
			throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

		return responseDictionary;
	}

	public async Task<Dictionary<string, object>?> GetStockPriceQuote(string stockSymbol) {
		HttpClient httpClient = httpClientFactory.CreateClient();

		string url = $"https://finnhub.io/api/v1/quote?symbol={stockSymbol}&token={configuration["FinnhubToken"]}";


		string responseBody = await httpClient.GetStringAsync(url);

		Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

		if (responseDictionary == null)
			throw new InvalidOperationException("No response from server");

		if (responseDictionary.ContainsKey("error"))
			throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

		return responseDictionary;
	}

	public async Task<List<Dictionary<string, string>>?> GetStocks() {
		HttpClient httpClient = httpClientFactory.CreateClient();

		string url = $"https://finnhub.io/api/v1/stock/symbol?exchange=US&token={configuration["FinnhubToken"]}";
		var responseBody = await httpClient.GetStringAsync(url);

		List<Dictionary<string, string>>? responseDictionary = JsonSerializer.Deserialize<List<Dictionary<string, string>>>(responseBody);

		if (responseDictionary is null)
			throw new InvalidOperationException("No response from server");

		return responseDictionary;
	}

	public async Task<Dictionary<string, object>?> SearchStocks(string stockSymbolToSearch) {
		HttpClient httpClient = httpClientFactory.CreateClient();

		string url = $"https://finnhub.io/api/v1/search?q={stockSymbolToSearch}&token={configuration["FinnhubToken"]}";

		string responseBody = await httpClient.GetStringAsync(url);

		Dictionary<string, object>? responseDictionary = JsonSerializer.Deserialize<Dictionary<string, object>>(responseBody);

		if (responseDictionary is null)
			throw new InvalidOperationException("No response from server");

		if (responseDictionary.ContainsKey("error"))
			throw new InvalidOperationException(Convert.ToString(responseDictionary["error"]));

		return responseDictionary;
	}
}