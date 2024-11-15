using MediatR;
using Microsoft.Extensions.Logging;
using WebAPIServer.Modules.Payment.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Queries
{
    public class CallBackVnPayQueryHandler : IRequestHandler<CallBackVnPayQuery,VnPayCallbackDto>
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly ILogger<CallBackVnPayQueryHandler> _logger;

        public CallBackVnPayQueryHandler(
            IPaymentRepository paymentRepository, 
            ILogger<CallBackVnPayQueryHandler> logger)
        {
            _paymentRepository = paymentRepository;
            _logger = logger;
        }

        public async Task<VnPayCallbackDto> Handle(CallBackVnPayQuery request, CancellationToken cancellationToken)
        {
            var paymentData = _paymentRepository.PaymentExecute(request.collection);
            return paymentData;
        }
    }
}
