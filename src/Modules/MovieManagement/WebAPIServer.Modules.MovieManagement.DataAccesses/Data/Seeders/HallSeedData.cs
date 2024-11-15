using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.MovieManagement.Domain.Entities;

namespace WebAPIServer.Modules.MovieManagement.DataAccesses.Data.Seeders
{
	internal static class HallSeedData
	{
		public static void Initialize(this IServiceProvider serviceProvider)
		{
			using (var context = new MovieManagementDbContext(serviceProvider
				.GetRequiredService<DbContextOptions<MovieManagementDbContext>>()))
			{
				if (!context.Halls.Any())
				{
					context.Halls.AddRange(
						new Hall
						{
							Id = Guid.Parse("7a8d4dea-15dd-4cdd-ad10-0ae010106891"),
							Name = "Rạp 1",
							Seats = new List<Seat>
							{
								new Seat
								{
									Id = Guid.Parse("9fe4f120-4c3c-46c2-b612-a47d4efcb7d7"),
									SeatRow = 1,
									SeatColumn = 1,
									SeatPosition = Seat.GetSeatPosition(1, 1),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
									SeatTypePrice = 55000
								},new Seat
								{
									Id = Guid.Parse("ea6bb358-6607-489c-adf6-79dc5931ec6a"),
									SeatRow = 2,
									SeatColumn = 1,
									SeatPosition = Seat.GetSeatPosition(2, 1),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
									SeatTypePrice = 55000
								},new Seat
								{
									Id = Guid.Parse("20fdf39b-6285-404d-8a40-27d05dbb1e17"),
									SeatRow = 3,
									SeatColumn = 1,
									SeatPosition = Seat.GetSeatPosition(3, 1),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
									SeatTypePrice = 55000
								},new Seat
								{
									Id = Guid.Parse("feb19652-cb86-40f2-9dac-5474efc071ed"),
									SeatRow = 1,
									SeatColumn = 2,
									SeatPosition = Seat.GetSeatPosition(1, 2),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								},new Seat
								{
									Id = Guid.Parse("1df756b8-655d-4613-9182-dcee29ad277c"),
									SeatRow = 2,
									SeatColumn = 2,
									SeatPosition = Seat.GetSeatPosition(2, 2),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								}, new Seat
								{
									Id = Guid.Parse("90fcc64b-f777-46a3-9f5e-d449d8f25c1e"),
									SeatRow = 3,
									SeatColumn = 2,
									SeatPosition = Seat.GetSeatPosition(3, 2),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								}
							},
						},
						new Hall
						{
							Id = Guid.Parse("7a8d4dea-15dd-4cdd-ad10-0ae010106892"),
							Name = "Rạp 2",
							Seats = new List<Seat>
							{
								new Seat
								{
									Id = Guid.NewGuid(),
									SeatRow = 1,
									SeatColumn = 1,
									SeatPosition = Seat.GetSeatPosition(1, 1),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								},new Seat
								{
									Id = Guid.NewGuid(),
									SeatRow = 2,
									SeatColumn = 1,
									SeatPosition = Seat.GetSeatPosition(2, 1),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								},new Seat
								{
									Id = Guid.NewGuid(),
									SeatRow = 3,
									SeatColumn = 1,
									SeatPosition = Seat.GetSeatPosition(3, 1),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								},new Seat
								{
									Id = Guid.NewGuid(),
									SeatRow = 1,
									SeatColumn = 2,
									SeatPosition = Seat.GetSeatPosition(1, 2),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								},new Seat
								{
									Id = Guid.NewGuid(),
									SeatRow = 2,
									SeatColumn = 2,
									SeatPosition = Seat.GetSeatPosition(2, 2),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								}, new Seat
								{
									Id = Guid.NewGuid(),
									SeatRow = 3,
									SeatColumn = 2,
									SeatPosition = Seat.GetSeatPosition(3, 2),
									SeatTypeId = SeatTypeConstants.Regular,
									SeatTypeName = "Ghế thường",
                                    SeatTypePrice = 55000
								}
							}
						}
					);
					context.SaveChanges();
				}
			}
		}
	}
}
