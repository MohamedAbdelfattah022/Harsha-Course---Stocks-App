using Entities;
using RepositoryContracts;
using ServiceContracts;
using ServiceContracts.DTO;
using Services;

namespace Tests {
	public class StocksServiceTest {
		private readonly IStocksService _stocksService;
		private readonly IStocksRepository _stocksRepository = Substitute.For<IStocksRepository>();
		private readonly IFixture _fixture;

		public StocksServiceTest() {
			_stocksService = new StocksService(_stocksRepository);
			_fixture = new Fixture();
		}


		#region CreateBuyOrder

		[Fact]
		public async Task CreateBuyOrder_NullBuyOrder_ShouldBeArgumentNullException() {
			//Arrange
			BuyOrderRequest? buyOrderRequest = null;

			//Act
			Func<Task> result = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);

			//Assert
			await result.Should().ThrowAsync<ArgumentNullException>();
		}


		[Theory]
		[InlineData(0)]
		public async Task CreateBuyOrder_QuantityIsLessThanMinimum_ShouldBeArgumentException(uint buyOrderQuantity) {
			//Arrange
			var buyOrderRequest = new BuyOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					Price = 1,
					Quantity = buyOrderQuantity
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);
			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Theory]
		[InlineData(100001)]
		public async Task CreateBuyOrder_QuantityIsGreaterThanMaximum_ShouldBeArgumentException(uint buyOrderQuantity) {
			//Arrange
			var buyOrderRequest = new BuyOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					Price = 1,
					Quantity = buyOrderQuantity
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Theory]
		[InlineData(0)]
		public async Task CreateBuyOrder_PriceIsLessThanMinimum_ShouldBeArgumentException(uint buyOrderPrice) {
			//Arrange
			var buyOrderRequest = new BuyOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					Price = buyOrderPrice,
					Quantity = 1
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Theory]
		[InlineData(10001)]
		public async Task CreateBuyOrder_PriceIsGreaterThanMaximum_ShouldBeArgumentException(uint buyOrderPrice) {
			//Arrange
			var buyOrderRequest = new BuyOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					Price = buyOrderPrice,
					Quantity = 1
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrder_StockSymbolIsNull_ShouldBeArgumentException() {
			//Arrange
			var buyOrderRequest = new BuyOrderRequest()
				{
					StockSymbol = null,
					Price = 1,
					Quantity = 1
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrder_DateOfOrderIsLessThanYear2000_ShouldBeArgumentException() {
			//Arrange
			var buyOrderRequest = new BuyOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"),
					Price = 1,
					Quantity = 1
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateBuyOrder(buyOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateBuyOrder_ValidData_ShouldBeSuccessful() {
			//Arrange
			var buyOrderRequest = new BuyOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"),
					Price = 1,
					Quantity = 1
				};

			//Act
			var buyOrderResponseFromCreate = await _stocksService.CreateBuyOrder(buyOrderRequest);

			//Assert
			buyOrderResponseFromCreate.BuyOrderID.Should().NotBe(Guid.Empty);
		}

		#endregion

		#region CreateSellOrder

		[Fact]
		public async Task CreateSellOrder_NullSellOrder_ShouldBeArgumentNullException() {
			//Arrange
			SellOrderRequest? sellOrderRequest = null;

			//Act
			Func<Task> result = async () => await _stocksService.CreateSellOrder(sellOrderRequest);

			//Assert
			await result.Should().ThrowAsync<ArgumentNullException>();
		}

		[Theory]
		[InlineData(0)]
		public async Task CreateSellOrder_QuantityIsLessThanMinimum_ShouldBeArgumentException(uint sellOrderQuantity) {
			//Arrange
			var sellOrderRequest = new SellOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					Price = 1,
					Quantity = sellOrderQuantity
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateSellOrder(sellOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}


		[Theory]
		[InlineData(100001)]
		public async Task CreateSellOrder_QuantityIsGreaterThanMaximum_ShouldBeArgumentException(uint sellOrderQuantity) {
			//Arrange
			var sellOrderRequest = new SellOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					Price = 1,
					Quantity = sellOrderQuantity
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateSellOrder(sellOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Theory]
		[InlineData(0)]
		public async Task CreateSellOrder_PriceIsLessThanMinimum_ShouldBeArgumentException(uint sellOrderPrice) {
			//Arrange
			var sellOrderRequest = new SellOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					Price = sellOrderPrice,
					Quantity = 1
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateSellOrder(sellOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Theory]
		[InlineData(10001)]
		public async Task CreateSellOrder_PriceIsGreaterThanMaximum_ShouldBeArgumentException(uint sellOrderPrice) {
			//Arrange
			var sellOrderRequest = new SellOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					Price = sellOrderPrice,
					Quantity = 1
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateSellOrder(sellOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrder_StockSymbolIsNull_ShouldBeArgumentException() {
			//Arrange
			var sellOrderRequest = new SellOrderRequest()
				{
					StockSymbol = null,
					Price = 1,
					Quantity = 1
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateSellOrder(sellOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrder_DateOfOrderIsLessThanYear2000_ShouldBeArgumentException() {
			//Arrange
			var sellOrderRequest = new SellOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					DateAndTimeOfOrder = Convert.ToDateTime("1999-12-31"),
					Price = 1, Quantity = 1
				};

			//Act
			Func<Task> result = async () => await _stocksService.CreateSellOrder(sellOrderRequest);

			// Assert
			await result.Should().ThrowAsync<ArgumentException>();
		}

		[Fact]
		public async Task CreateSellOrder_ValidData_ShouldBeSuccessful() {
			//Arrange
			var sellOrderRequest = new SellOrderRequest()
				{
					StockSymbol = "MSFT",
					StockName = "Microsoft",
					DateAndTimeOfOrder = Convert.ToDateTime("2024-12-31"),
					Price = 1,
					Quantity = 1
				};

			//Act
			var sellOrderResponseFromCreate = await _stocksService.CreateSellOrder(sellOrderRequest);

			//Assert
			sellOrderResponseFromCreate.SellOrderID.Should().NotBe(Guid.Empty);
		}

		#endregion

		#region GetBuyOrders

		[Fact]
		public async Task GetAllBuyOrders_DefaultList_ShouldBeEmpty() {
			//Arrange
			_stocksRepository.GetBuyOrders().Returns([]);

			//Act
			var buyOrdersFromGet = await _stocksService.GetBuyOrders();

			//Assert
			buyOrdersFromGet.Should().BeEmpty();
		}

		[Fact]
		public async Task GetAllBuyOrders_WithFewBuyOrders_ShouldBeSuccessful() {
			//Arrange
			var buyOrders = _fixture.CreateMany<BuyOrder>(3).ToList();
			_stocksRepository.GetBuyOrders().Returns(Task.FromResult(buyOrders));

			// Act
			var buyOrdersFromGet = await _stocksService.GetBuyOrders();

			// Assert
			buyOrdersFromGet.Should().BeEquivalentTo(buyOrders);
		}

		#endregion

		#region GetSellOrders

		[Fact]
		public async Task GetAllSellOrders_DefaultList_ShouldBeEmpty() {
			// Arrange
			_stocksRepository.GetSellOrders().Returns([]);

			//Act
			var sellOrdersFromGet = await _stocksService.GetSellOrders();

			//Assert
			sellOrdersFromGet.Should().BeEmpty();
		}


		[Fact]
		public async Task GetAllSellOrders_WithFewSellOrders_ShouldBeSuccessful() {
			//Arrange
			var sellOrders = _fixture.CreateMany<SellOrder>(3).ToList();

			_stocksRepository.GetSellOrders().Returns(Task.FromResult(sellOrders));

			//Act
			var sellOrdersFromGet = await _stocksService.GetSellOrders();

			//Assert
			sellOrdersFromGet.Should().BeEquivalentTo(sellOrders);
		}

		#endregion
	}
}