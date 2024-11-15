﻿namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos
{
    public class VnPayCallbackDto
    {
        public string OrderDescription { get; set; }
        public string TransactionId { get; set; }
        public Guid OrderId { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentId { get; set; }
        public bool Success { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
        public double VnPayAmount { get; set; }
        public string BankCode { get; set; }
        public string CardType { get; set; }
    }
}
