using AutoMapper;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Modules.Booking.Domain.Entities;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder
{
	public class OrderProfile : Profile
	{
		public OrderProfile()
		{
			Init();
		}

		private void Init()
		{
			CreateMap<Order, OrderForViewDto>();
			CreateMap<Order, OrderForViewDetailsDto>()
				.ForMember(src => src.ShowSeats, opt => opt.MapFrom(src => src.ShowSeats))
				.ForMember(src => src.Products, opt => opt.MapFrom(src => src.Products))
				.ForMember(src => src.Combos, opt => opt.MapFrom(src => src.Combos));

			CreateMap<OrderCombo, OrderComboForViewDto>()
				.ForMember(src => src.Id, opt => opt.MapFrom(src => src.ComboId))
				.ForMember(src => src.Name, opt => opt.MapFrom(src => src.ComboName));

			CreateMap<OrderProduct, OrderProductForViewDto>()
				.ForMember(src => src.Id, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(src => src.Name, opt => opt.MapFrom(src => src.ProductName));

			CreateMap<OrderShowSeat, OrderShowSeatForViewDto>()
				.ForMember(src => src.Id, opt => opt.MapFrom(src => src.SeatId))
				.ForMember(src => src.Position, opt => opt.MapFrom(src => src.SeatPosition));

			CreateMap<OrderForCreateDto, Order>()
				.ForMember(src => src.ShowSeats, opt => opt.MapFrom(src => src.ShowSeats))
				.ForMember(src => src.Products, opt => opt.MapFrom(src => src.Products))
				.ForMember(src => src.Combos, opt => opt.MapFrom(src => src.Combos));
			CreateMap<OrderProductForCreateDto, OrderProduct>();
			CreateMap<OrderComboForCreateDto, OrderCombo>();
/*			CreateMap<OrderTicketTypeForCreateDto, OrderTicketType>()
				.ForMember(dest => dest.TicketTypeId, opt => opt.MapFrom(src => src.TicketTypeId));*/
			CreateMap<OrderShowSeatForCreateDto, OrderShowSeat>();

			// Mapping details for each individual OrderProduct item
			CreateMap<OrderProductForCreateDto, OrderProduct>()
				.ForMember(dest => dest.ProductId, opt => opt.MapFrom(src => src.ProductId))
				.ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));
			CreateMap<OrderForUpdateDto, Order>();
		}
	}
}