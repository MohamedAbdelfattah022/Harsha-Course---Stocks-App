using AngleSharp.Html.Parser;
using Microsoft.AspNetCore.Mvc.Testing;
using StockMarketSolution;

namespace StockMarket.Tests.Integration;

public class StocksControllerTests(WebApplicationFactory<IApiMarker> appFactory) : IClassFixture<WebApplicationFactory<IApiMarker>> {
	private readonly HttpClient _client = appFactory.CreateClient();
	private readonly IHtmlParser _parser = new HtmlParser();

	[Fact]
	public async Task Explore_WithNullStock_ShouldReturnViewWithStocksList()
	{
		// Act
		var response = await _client.GetAsync("/Stocks/Explore");
		response.EnsureSuccessStatusCode();

		var responseContent = await response.Content.ReadAsStringAsync();
		var document = await _parser.ParseDocumentAsync(responseContent);

		// Assert
		var titleElement = document.QuerySelector("title")?.TextContent;
		titleElement.Should().Contain("Explore");

		var stockListItems = document.QuerySelectorAll("#stocks-list .list li");
		stockListItems.Should().NotBeEmpty("The model should contain a list of stocks");

	}
}