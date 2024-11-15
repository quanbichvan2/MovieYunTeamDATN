using AutoMapper;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Modules.Tickets.Domain.Entities;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType
{
	public class TicketProfile : Profile
	{
		public TicketProfile()
		{
			Init();
		}
		private void Init()
		{
			CreateMap<TicketType, TicketTypeForViewDto>();
			CreateMap<TicketTypeForCreateDto, TicketType>();
			CreateMap<TicketTypeForUpdateDto, TicketType>();
		}
	}
}