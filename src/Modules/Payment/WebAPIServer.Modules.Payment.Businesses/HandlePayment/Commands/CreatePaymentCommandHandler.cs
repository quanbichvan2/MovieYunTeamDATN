using MediatR;
using Microsoft.Extensions.Options;
using Stripe.Checkout;
using Stripe.V2;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Commands
{
    public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, string>
    {
        private readonly StripeSettings _stripeSettings;
        public string SessionId { get; set; }

        public CreatePaymentCommandHandler(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        public async Task<string> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
        {
            try
            {
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
                var options = new PaymentIntentCreateOptions
                {
                    Amount = request.mode.Amount,
                    Currency = "vnd",
                    PaymentMethodTypes = new List<string> { "card" }
                };

                var service = new PaymentIntentService();
                var paymentIntent = await service.CreateAsync(options);

                return paymentIntent.ClientSecret;
            }
            catch (StripeException ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
