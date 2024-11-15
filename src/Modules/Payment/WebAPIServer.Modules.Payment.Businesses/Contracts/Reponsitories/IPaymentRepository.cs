using Microsoft.AspNetCore.Http;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;
using WebAPIServer.Modules.Payment.Domain.Entities.Payment;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Payment.Businesses.Contracts.Reponsitories
{
    public interface IPaymentRepository : IRepository<PaymentTransaction>
    {
        VnPayCallbackDto PaymentExecute(IQueryCollection collections);
    }
}
