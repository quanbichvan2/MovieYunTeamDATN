/*using MediatR;
using Microsoft.Extensions.Logging;
using OneOf;
using WebAPIServer.Modules.Payment.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Payment.Domain.Entities.Payment;
using WebAPIServer.Shared.Abstractions.Enums;
using WebAPIServer.Shared.Abstractions.Exceptions;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Commands
{
    public class CreatePaymentMethodCashCommandHanlder : IRequestHandler<CreatePaymentMethodCashCommand, OneOf<Guid, ResponseException>>
    {
        private readonly ILogger<CreatePaymentMethodCashCommandHanlder> _logger;
        private readonly IPaymentRepository _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreatePaymentMethodCashCommandHanlder(
            ILogger<CreatePaymentMethodCashCommandHanlder> logger,
            IPaymentRepository paymentRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<OneOf<Guid, ResponseException>> Handle(CreatePaymentMethodCashCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var payment = new PaymentTransaction
                {
                    PaymentMethod = "Cash",
                    Success = true,
                    Amount = request.Model.Amount,
                    TransactionDate = DateTime.UtcNow,
                    PaymentStatus = "Success",
                    OrderId = "",
                    TransactionId = "",
                    PaymentId = "",
                    Token = "",
                    VnPayResponseCode = "",
                    BankCode = "",
                    CardType = ""
                };
                await _paymentRepository.CreateAsync(payment);
                await _unitOfWork.SaveChangesAsync();
                return payment.Id;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return ResponseExceptionHelper.ErrorResponse<PaymentTransaction>(ErrorCode.OperationFailed);
            }
        }
    }
}
*/