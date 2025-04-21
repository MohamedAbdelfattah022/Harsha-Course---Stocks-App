using Microsoft.EntityFrameworkCore;

namespace Entities;

public class OrdersDbContext(DbContextOptions<OrdersDbContext> options) : DbContext(options) {
	public DbSet<BuyOrder> BuyOrders { get; set; }
	public DbSet<SellOrder> SellOrders { get; set; }
}