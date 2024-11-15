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
	public class UpdateVoucherCommandHandler : IRequestHandler<UpdateVoucherCommand, OneOf<bool, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<UpdateVoucherCommandHandler> _logger;
		private readonly IVoucherRepository _voucherRepository;
		private readonly IValidator<VoucherForUpdateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public UpdateVoucherCommandHandler(IMapper mapper,
			ILogger<UpdateVoucherCommandHandler> logger,
			IVoucherRepository voucherRepository,
			IValidator<VoucherForUpdateDto> validator,
			IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_logger = logger;
			_voucherRepository = voucherRepository;
			_validator = validator;
			_unitOfWork = unitOfWork;
		}

		public async Task<OneOf<bool, ResponseException>> Handle(UpdateVoucherCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var validationResult = await _validator.ValidateAsync(request.Model);
				if (!validationResult.IsValid)
				{
					return ResponseExceptionHelper.ErrorResponse<Voucher>(ErrorCode.UpdateError, validationResult.Errors);
				}
				var voucher = await _voucherRepository.GetByIdAsync(request.Id);
				if (voucher == null)
				{
					return ResponseExceptionHelper.ErrorResponse<Voucher>(ErrorCode.NotFound, validationResult.Errors);
				}

				var validationname = await _voucherRepository.IsNameExistsAsync(voucher.Name, request.Id);
				if (validationname == true)
				{
					return ResponseExceptionHelper.ErrorResponse<Voucher>(ErrorCode.UpdateError, validationResult.Errors);
				}
				voucher.CreatedAt = DateTime.UtcNow;
				_mapper.Map(request.Model, voucher);
				_voucherRepository.Update(voucher);
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
