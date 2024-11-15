using MediatR;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Commands
{
    public record CreatePaymentMethodCashCommand() : IRequest<string>;
}
