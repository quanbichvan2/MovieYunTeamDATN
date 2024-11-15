using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using WebAPIServer.Modules.Payment.Businesses.Contracts.Libraries;
using WebAPIServer.Modules.Payment.Businesses.Contracts.Reponsitories;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;
using WebAPIServer.Modules.Payment.DataAccesses.Data;
using WebAPIServer.Modules.Payment.Domain.Entities;
using WebAPIServer.Modules.Payment.Domain.Entities.Payment;
using WebAPIServer.Shared.Abstractions.Repositories;

namespace WebAPIServer.Modules.Payment.DataAccesses.Repositories
{
	public class PaymentRepository : BaseRepository<PaymentTransaction, PaymentDbContext>, IPaymentRepository
	{
        private readonly IConfiguration _configuration;
        public PaymentRepository(PaymentDbContext context, IConfiguration configuration) : base(context)
        {
            _configuration = configuration;
        }

        public VnPayCallbackDto PaymentExecute(IQueryCollection collections)
        {
            var pay = new VnPayLibrary();
            var response = pay.GetFullResponseData(collections, _configuration["Vnpay:HashSecret"]);
            var status = response.VnPayResponseCode == "00" ? "Success" : "Fail";
            var PaymentTransaction = new PaymentTransaction
            {
                TransactionId = response.TransactionId,
                PaymentId = response.PaymentId,
                Success = response.Success,
                Amount = response.VnPayAmount,
                TransactionDate = DateTime.UtcNow,
                PaymentStatus =  status,
                PaymentMethod = response.PaymentMethod,
                PaymentMethodId = PaymentMethodContans.CreditCard,
                OrderId = response.OrderId,
            };
            _context.PaymentTransactions.Add(PaymentTransaction);
            _context.SaveChanges();
            return response;
        }
    }
}
