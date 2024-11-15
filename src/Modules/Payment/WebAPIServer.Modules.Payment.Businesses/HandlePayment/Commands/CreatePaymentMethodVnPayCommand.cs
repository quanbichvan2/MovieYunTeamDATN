using MediatR;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Commands
{
    public record CreatePaymentMethodVnPayCommand(Guid OrderId): IRequest<string>;
}
