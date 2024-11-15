using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Booking.Domain.Entities;

namespace WebAPIServer.Modules.Booking.DataAccesses.Data.Seeders
{
	public static class OrderStatusSeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new BookingDbContext(serviceProvider.GetRequiredService<DbContextOptions<BookingDbContext>>()))
			{
				if (!context.OrderStatuses.Any())
				{
					context.OrderStatuses.AddRange(
						new OrderStatus
						{
							Id = OrderStatusConstants.Requested,
							Name = "Requested"
						}, new OrderStatus
						{
							Id = OrderStatusConstants.Pending,
							Name = "Pending"
						}, new OrderStatus
						{
							Id = OrderStatusConstants.Confirmed,
							Name = "Confirmed"
						}, new OrderStatus
						{
							Id = OrderStatusConstants.CheckedIn,
							Name = "CheckedIn"
						}, new OrderStatus
						{
							Id = OrderStatusConstants.Canceled,
							Name = "Canceled"
						}, new OrderStatus
						{
							Id = OrderStatusConstants.Abandoned,
							Name = "Abandoned"
						}
					);
					context.SaveChanges();
				}
			}
		}
	}
}
