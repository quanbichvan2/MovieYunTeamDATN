using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Modules.Tickets.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Queries
{
	public class GetTicketTypeByIdQueryHandler : IRequestHandler<GetTicketTypeByIdQuery, OneOf<TicketTypeForViewDto, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ITicketTypeRepository _ticketTypeRepository;
		private readonly ILogger<GetTicketTypeByIdQueryHandler> _logger;
		public GetTicketTypeByIdQueryHandler(IMapper mapper,
			ITicketTypeRepository ticketTypeRepository,
			ILogger<GetTicketTypeByIdQueryHandler> logger)
		{
			_mapper = mapper;
			_ticketTypeRepository = ticketTypeRepository;
			_logger = logger;
		}
		public async Task<OneOf<TicketTypeForViewDto, ResponseException>> Handle(GetTicketTypeByIdQuery request, CancellationToken cancellationToken)
		{
			try
			{
				var ticket = await _ticketTypeRepository.FindByIdAsync(request.Id);
				if (ticket is null)
				{
					return ResponseExceptionHelper.ErrorResponse<TicketType>(ErrorCode.NotFound);
				}
				return _mapper.Map<TicketTypeForViewDto>(ticket);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<TicketType>(ErrorCode.OperationFailed);
			}
		}
	}
}
