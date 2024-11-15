using WebAPIServer.Shared.Abstractions.Entities;

namespace WebAPIServer.Modules.Payment.Domain.Entities
{
	public class PaymentMethod : BaseEntity
	{
		public string Name { get; set; } = default!;
	}
	public static class PaymentMethodContans
	{
		public static Guid Cash = Guid.Parse("688190b1-105f-4687-98cd-d1e8872a00a6");
		public static Guid CreditCard = Guid.Parse("86b951f0-38af-458c-ae8a-478bcf87a3aa");
	}
}