using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos
{
	public record OrderDto
	{
		public Guid Id { get; set; }
		public double Amount { get; set; }
		public string? OrderDescription { get; set; }

		public static OrderDto FromBytes(byte[] tradeAsBytes)
		{
			var trade = Encoding.UTF8.GetString(tradeAsBytes) ?? string.Empty;
			return JsonSerializer.Deserialize<OrderDto>(trade) ??
				throw NewDeserializationException(
					from: $"{nameof(tradeAsBytes)} {tradeAsBytes.GetType().Name}",
					to: $"{typeof(OrderDto).Name}");
		}
		private static SerializationException NewDeserializationException(string from, string to) =>
			new SerializationException($"Deserialization from '{from}' to '{to}' failed.");
	}
}
