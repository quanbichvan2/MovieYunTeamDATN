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
	public class DeleteVoucherCommandHandler : IRequestHandler<DeleteVoucherCommand, OneOf<bool, ResponseException>>
	{
		private readonly IMapper _mapper;
		private readonly ILogger<DeleteVoucherCommandHandler> _logger;
		private readonly IVoucherRepository _voucherRepository;
		private readonly IValidator<VoucherForCreateDto> _validator;
		private readonly IUnitOfWork _unitOfWork;
		public DeleteVoucherCommandHandler(IMapper mapper, ILogger<DeleteVoucherCommandHandler> logger, IVoucherRepository voucherRepository, IValidator<VoucherForCreateDto> validator, IUnitOfWork unitOfWork)
		{
			_mapper = mapper;
			_logger = logger;
			_voucherRepository = voucherRepository;
			_validator = validator;
			_unitOfWork = unitOfWork;
		}

		public async Task<OneOf<bool, ResponseException>> Handle(DeleteVoucherCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var voucher = await _voucherRepository.FindByIdAsync(request.Id);
				if (voucher != null)
				{
					if (voucher.Id != request.Id)
					{
						return ResponseExceptionHelper.ErrorResponse<Voucher>(ErrorCode.Existed);
					}
					_voucherRepository.Delete(voucher);
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
