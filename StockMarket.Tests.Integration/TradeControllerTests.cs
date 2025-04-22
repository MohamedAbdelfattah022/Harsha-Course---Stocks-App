using Microsoft.AspNetCore.Mvc.Testing;
using StockMarketSolution;
using AngleSharp.Html.Parser;
using FluentAssertions;

namespace StockMarket.Tests.Integration;

public class TradeControllerTests(WebApplicationFactory<IApiMarker> appFactory) : IClassFixture<WebApplicationFactory<IApiMarker>> {
	private readonly HttpClient _client = appFactory.CreateClient();

	[Theory]
	[InlineData("MSFT")]
	public async Task Get_ViewContainsPriceClass_WhenValidStockSymbol(string stockSymbol) {
		// Act
		var response = await _client.GetAsync($"/Trade/Index/{stockSymbol}");
		response.EnsureSuccessStatusCode();

		var htmlContent = await response.Content.ReadAsStringAsync();
		var parser = new HtmlParser();
		var document = await parser.ParseDocumentAsync(htmlContent);


		// Assert
		var priceElement = document.QuerySelector(".price");
		priceElement.Should().NotBeNull();
	}
}