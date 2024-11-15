using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Queries
{
	public class GetAllTicketTypesQueryHandler : IRequestHandler<GetAllTicketTypesQuery, IEnumerable<TicketTypeForViewDto>>
	{
		private readonly IMapper _mapper;
		private readonly ITicketTypeRepository _ticketTypeRepository;
		private readonly ILogger<GetAllTicketTypesQueryHandler> _logger;
		public GetAllTicketTypesQueryHandler(IMapper mapper,
			ITicketTypeRepository ticketTypeRepository,
			ILogger<GetAllTicketTypesQueryHandler> logger)
		{
			_mapper = mapper;
			_ticketTypeRepository = ticketTypeRepository;
			_logger = logger;
		}
		public async Task<IEnumerable<TicketTypeForViewDto>> Handle(GetAllTicketTypesQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var ticketType = await _ticketTypeRepository.GetAll().ToListAsync(cancellationToken);
				var ticketTypeViewDtos = _mapper.Map<List<TicketTypeForViewDto>>(ticketType);
				return ticketTypeViewDtos;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				throw new NullReferenceException(nameof(Handle));
			}
		}
	}
}
