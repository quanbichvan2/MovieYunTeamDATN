using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Commands
{
    public record CreatePaymentCommand(PaymentRequest mode) : IRequest<string>;
}
