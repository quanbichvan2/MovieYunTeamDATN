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
	public class CreateTicketTypeCommandHandler : IRequestHandler<CreateTicketTypeCommand, OneOf<Guid, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<CreateTicketTypeCommandHandler> _logger;
		private readonly ITicketTypeRepository _ticketTypeRepository;
		private readonly IValidator<TicketTypeForCreateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public CreateTicketTypeCommandHandler(IMapper mapper,
			ILogger<CreateTicketTypeCommandHandler> logger,
			ITicketTypeRepository ticketTypeRepository,
			IValidator<TicketTypeForCreateDto> validator,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_logger = logger;
			_ticketTypeRepository = ticketTypeRepository;
			_validator = validator;
			_unitOfWork = unitOfWork;
		}
		public async Task<OneOf<Guid, ResponseException>> Handle(CreateTicketTypeCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<TicketType>(ErrorCode.CreateError, validationResult.Errors);
				}
				TicketType ticket = _mapper.Map<TicketType>(request.Model);
				ticket.CreatedAt = DateTime.UtcNow;
				ticket.ModifiedAt = DateTime.UtcNow;
				await _ticketTypeRepository.CreateAsync(ticket);
				await _unitOfWork.SaveChangesAsync();
				return ticket.Id;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<TicketType>(ErrorCode.OperationFailed);
			}
		}
	}
}
