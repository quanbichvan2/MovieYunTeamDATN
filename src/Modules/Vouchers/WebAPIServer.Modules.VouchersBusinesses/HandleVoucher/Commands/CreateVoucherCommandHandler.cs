using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Vouchers.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Modules.Vouchers.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Commands
{
	public class CreateVoucherCommandHandler : IRequestHandler<CreateVoucherCommand, OneOf<Guid, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<CreateVoucherCommandHandler> _logger;
		private readonly IVoucherRepository _voucherRepository;
		private readonly IValidator<VoucherForCreateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public CreateVoucherCommandHandler(IMapper mapper,
			ILogger<CreateVoucherCommandHandler> logger,
			IVoucherRepository voucherRepository,
			IValidator<VoucherForCreateDto> validator,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_logger = logger;
			_voucherRepository = voucherRepository;
			_validator = validator;
			_unitOfWork = unitOfWork;
		}

		public async Task<OneOf<Guid, ResponseException>> Handle(CreateVoucherCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<Voucher>(ErrorCode.CreateError, validationResult.Errors);
				}

				var ticket = _mapper.Map<Voucher>(request.Model);
				ticket.CreatedAt = DateTime.UtcNow;
				ticket.ModifiedAt = DateTime.UtcNow;

				await _voucherRepository.CreateAsync(ticket);
				await _unitOfWork.SaveChangesAsync();
				return ticket.Id;
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
				return ResponseExceptionHelper.ErrorResponse<Voucher>(ErrorCode.OperationFailed);
			}
		}
	}
}
