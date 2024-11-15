namespace WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models.Base
{
    public class VoucherBaseDto
    {
        public string? Code { get; set; } 
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDiscountPercentage { get; set; }
        public double DiscountValue { get; set; }
    }
}
