using System.ComponentModel.DataAnnotations.Schema;
using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Payment.Domain.Entities.Payment
{
    public class PaymentTransaction : BaseAuditableEntity
    {
        public Guid OrderId { get; set; }
        public string TransactionId { get; set; } = string.Empty;
        public string PaymentId { get; set; } = string.Empty;
        public bool Success { get; set; }
        public double Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string PaymentStatus {  get; set; }

        [ForeignKey("PaymentMethod")]
        public Guid PaymentMethodId { get; set; }
        public string PaymentMethod { get; set; }
    }
}
