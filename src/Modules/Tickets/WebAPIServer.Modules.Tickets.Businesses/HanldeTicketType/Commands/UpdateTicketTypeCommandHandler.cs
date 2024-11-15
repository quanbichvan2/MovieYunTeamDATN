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
	public class UpdateTicketTypeCommandHandler : IRequestHandler<UpdateTicketTypeCommand, OneOf<bool, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateTicketTypeCommandHandler> _logger;
		private readonly ITicketTypeRepository _ticketTypeRepository;
		private readonly IValidator<TicketTypeForUpdateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public UpdateTicketTypeCommandHandler(IMapper mapper,
			ILogger<UpdateTicketTypeCommandHandler> logger,
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

		public async Task<OneOf<bool, ResponseException>> Handle(UpdateTicketTypeCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<TicketType>(ErrorCode.UpdateError, validationResult.Errors);
				}

				var ticket = await _ticketTypeRepository.FindByIdAsync(request.Id);
				if (ticket == null)
				{
					return ResponseExceptionHelper.ErrorResponse<TicketType>(ErrorCode.NotFound, validationResult.Errors);
				}

				ticket.ModifiedAt = DateTime.UtcNow;
				_mapper.Map(request.Model, ticket);
				_ticketTypeRepository.Update(ticket);
				await _unitOfWork.SaveChangesAsync();
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
