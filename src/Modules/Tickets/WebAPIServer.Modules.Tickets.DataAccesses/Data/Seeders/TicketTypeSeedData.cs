using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Tickets.Domain.Entities;

namespace WebAPIServer.Modules.Tickets.DataAccesses.Data.Seeders
{
	public class TicketTypeSeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new TicketsDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<TicketsDbContext>>()))
			{
				if (!context.TicketTypes.Any())
				{
					context.TicketTypes.AddRange(
					new TicketType
					{
						Id = TicketTypeConstants.Regular,
						Code = "TK01",
						Name = "Vé Thường",
						Price = 0,
						CreatedAt = DateTime.UtcNow,
						ModifiedAt = DateTime.UtcNow
					},
					new TicketType
					{
						Id = TicketTypeConstants.RushHour,
						Code = "TK02",
						Name = "Vé Giờ Cao Điểm",
						Price = 20000,
						CreatedAt = DateTime.UtcNow,
						ModifiedAt = DateTime.UtcNow
					},
					new TicketType
					{
						Id = TicketTypeConstants.Vacation,
						Code = "TK03",
						Name = "Vé Ngày lễ",
						Price = 30000,
						CreatedAt = DateTime.UtcNow,
						ModifiedAt = DateTime.UtcNow
					});
					context.SaveChanges();
				}
			}

		}
	}
}
