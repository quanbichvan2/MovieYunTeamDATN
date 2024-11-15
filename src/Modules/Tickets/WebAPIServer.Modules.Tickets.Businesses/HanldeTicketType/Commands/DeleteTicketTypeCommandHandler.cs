using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Tickets.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Modules.Tickets.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Commands
{
	public class DeleteTicketTypeCommandHandler : IRequestHandler<DeleteTicketTypeCommand, OneOf<bool, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<DeleteTicketTypeCommandHandler> _logger;
		private readonly ITicketTypeRepository _ticketTypeRepository;
		private readonly IValidator<TicketTypeForUpdateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public DeleteTicketTypeCommandHandler(IMapper mapper,
			ILogger<DeleteTicketTypeCommandHandler> logger,
			ITicketTypeRepository ticketTypeRepository,
			IValidator<TicketTypeForUpdateDto> validator,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_logger = logger;
			_ticketTypeRepository = ticketTypeRepository;
			_validator = validator;
			_unitOfWork = unitOfWork;
		}
		public async Task<OneOf<bool, ResponseException>> Handle(DeleteTicketTypeCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var ticket = await _ticketTypeRepository.FindByIdAsync(request.Id);
				if (ticket != null)
				{
					if (ticket.Id != request.Id)
					{
						return ResponseExceptionHelper.ErrorResponse<TicketType>(ErrorCode.Existed);
					}
					_ticketTypeRepository.Delete(ticket);
					await _unitOfWork.SaveChangesAsync();
					return true;
				}

				return true;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				throw new NullReferenceException(nameof(Handle));
			}
		}
	}
}
