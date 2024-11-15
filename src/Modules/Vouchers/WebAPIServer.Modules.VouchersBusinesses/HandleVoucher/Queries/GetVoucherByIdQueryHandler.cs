using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Vouchers.Businesses.Contracts.Repositories;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Modules.Vouchers.Domain.Entities;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Queries
{
	public class GetVoucherByIdQueryHandler : IRequestHandler<GetVoucherByIdQuery, OneOf<VoucherForViewDto, ResponseException>>
    {
        private readonly IMapper _mapper;
        private readonly IVoucherRepository _voucherRepository;
        private readonly ILogger<GetVoucherByIdQueryHandler> _logger;
        public GetVoucherByIdQueryHandler(IMapper mapper, 
            IVoucherRepository voucherRepository,
            ILogger<GetVoucherByIdQueryHandler> logger)
        {
            _mapper = mapper;
            _voucherRepository = voucherRepository;
            _logger = logger;
        }
        public async Task<OneOf<VoucherForViewDto, ResponseException>> Handle(GetVoucherByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var voucher = await _voucherRepository.GetByIdAsync(request.Id);
                if (voucher is null)
                {
                    return ResponseExceptionHelper.ErrorResponse<Voucher>(ErrorCode.NotFound);
                }
                return _mapper.Map<VoucherForViewDto>(voucher);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<Voucher>(ErrorCode.OperationFailed);
            }
        }
    }
}
