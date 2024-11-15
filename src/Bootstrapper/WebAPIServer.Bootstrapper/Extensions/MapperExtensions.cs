using AutoMapper;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType;
using WebAPIServer.Modules.Users.Businesses.HandleUser;

namespace WebAPIServer.Bootstrapper.Extensions
{
	public static class MapperExtensions
	{
		public static IServiceCollection AddMapperIntialize(this IServiceCollection services)
		{

			var config = new MapperConfiguration(c =>
			{
				// MovieManagement Module
				c.AddProfile<CastMemberProfile>();
				c.AddProfile<DirectorProfile>();
				c.AddProfile<GenreProfile>();
				c.AddProfile<HallProfile>();
				c.AddProfile<SeatTypeProfile>();
				c.AddProfile<MovieProfile>();
				c.AddProfile<ShowProfile>();
				// Catalog Module
				c.AddProfile<ProductProfile>();
				c.AddProfile<CategoryProfile>();
				c.AddProfile<ComboProfile>();
				//TicketTypes Module
				c.AddProfile<TicketProfile>();

				//Booking Module
				c.AddProfile<OrderProfile>();
				//User
				c.AddProfile<UserProfile>();

			});
			services.AddSingleton<IMapper>(s => config.CreateMapper());
			return services;
		}
	}
}
