using ServiceContracts;
using ServiceContracts.DTO;
using Services.Helpers;
using System;
using Entities;
using RepositoryContracts;

namespace Services {
	public class StocksService(IStocksRepository stocksRepository) : IStocksService {
		public async Task<BuyOrderResponse> CreateBuyOrder(BuyOrderRequest? buyOrderRequest) {
			if (buyOrderRequest == null)
				throw new ArgumentNullException(nameof(buyOrderRequest));

			ValidationHelper.ModelValidation(buyOrderRequest);

			BuyOrder buyOrder = buyOrderRequest.ToBuyOrder();

			buyOrder.BuyOrderID = Guid.NewGuid();

			await stocksRepository.CreateBuyOrder(buyOrder);
			return buyOrder.ToBuyOrderResponse();
		}


		public async Task<SellOrderResponse> CreateSellOrder(SellOrderRequest? sellOrderRequest) {
			if (sellOrderRequest == null)
				throw new ArgumentNullException(nameof(sellOrderRequest));

			ValidationHelper.ModelValidation(sellOrderRequest);

			SellOrder sellOrder = sellOrderRequest.ToSellOrder();

			sellOrder.SellOrderID = Guid.NewGuid();

			await stocksRepository.CreateSellOrder(sellOrder);
			return sellOrder.ToSellOrderResponse();
		}


		public async Task<List<BuyOrderResponse>> GetBuyOrders() {
			var buyOrders = await stocksRepository.GetBuyOrders();
			return buyOrders.Select(order => order.ToBuyOrderResponse()).ToList();
		}


		public async Task<List<SellOrderResponse>> GetSellOrders() {
			var sellOrders = await stocksRepository.GetSellOrders();
			return sellOrders.Select(order => order.ToSellOrderResponse()).ToList();
		}
	}
}