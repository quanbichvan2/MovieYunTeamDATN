using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos
{
    public class PaymentRequest
    {
        public long Amount { get; set; }
        public string PaymentMethodId { get; set; }
    }
}
