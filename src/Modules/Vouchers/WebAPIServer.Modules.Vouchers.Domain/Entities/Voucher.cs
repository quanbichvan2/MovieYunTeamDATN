using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Vouchers.Domain.Entities
{
    public class Voucher : BaseAuditableEntity
    {
        public string Code { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public bool IsDiscountPercentage { get; set; } = false;
        public double DiscountValue { get; set; } = 0;
        public double GetDiscountAmount(double originalPrice)
        {
            if (originalPrice <= 0)
                return 0;

            if (IsDiscountPercentage)
            {
                return originalPrice * Math.Min(DiscountValue, 100) / 100;
            }
            else
            {
                return DiscountValue;
            }
        }
    }
    public static class VouchersConstants
    {
        /// <summary>
        /// học sinh
        /// </summary>
        public static Guid Student = Guid.Parse("e03a98c5-f3f7-485c-b0be-5e08b6602f82");
        /// <summary>
        /// mã giảm giá lễ hội
        /// </summary>
        public static Guid Holiday = Guid.Parse("bc27b7c6-5e57-4554-bef9-913cf38e43fe");
        /// <summary>
        /// cặp đôi
        /// </summary>
        public static Guid Couple = Guid.Parse("13601186-7ff5-4cdc-bbe8-b39250f46d84");
    }
}
