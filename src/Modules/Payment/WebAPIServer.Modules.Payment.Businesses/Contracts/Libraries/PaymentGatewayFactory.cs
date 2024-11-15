using Microsoft.Extensions.DependencyInjection;
using WebAPIServer.Modules.Payment.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Payment.Domain.Entities.Payment;

namespace WebAPIServer.Modules.Payment.Businesses.Contracts.Libraries
{
    public class PaymentGatewayFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public PaymentGatewayFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

       /* public IPaymentGateway GetPaymentGateway(PaymentMethod model)
        {
            return model switch
            {
                PaymentMethodType.VNPay => _serviceProvider.GetService<IPaymentVnPayRepository>(),
                _ => throw new ArgumentException("Unsupported payment method")
            } ;
        }*/
    }
}
