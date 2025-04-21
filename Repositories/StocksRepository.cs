using Entities;
using Microsoft.EntityFrameworkCore;
using RepositoryContracts;

namespace Repositories;

public class StocksRepository(OrdersDbContext dbContext) : IStocksRepository {
	public async Task<BuyOrder> CreateBuyOrder(BuyOrder buyOrder) {
		dbContext.BuyOrders.Add(buyOrder);
		await dbContext.SaveChangesAsync();
		return buyOrder;
	}

	public async Task<SellOrder> CreateSellOrder(SellOrder sellOrder) {
		dbContext.SellOrders.Add(sellOrder);
		await dbContext.SaveChangesAsync();
		return sellOrder;
	}

	public async Task<List<BuyOrder>> GetBuyOrders() {
		var buyOrders = await dbContext.BuyOrders
			.OrderByDescending(temp => temp.DateAndTimeOfOrder)
			.AsNoTracking()
			.ToListAsync();

		return buyOrders;
	}

	public async Task<List<SellOrder>> GetSellOrders() {
		var sellOrders = await dbContext.SellOrders
			.OrderByDescending(o => o.DateAndTimeOfOrder)
			.AsNoTracking()
			.ToListAsync();

		return sellOrders;
	}
}