using MediatR;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Immutable;
using System.Text;
using WebAPIServer.Modules.Payment.Businesses.Contracts.Libraries;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;

namespace WebAPIServer.Modules.Payment.Businesses.HandlePayment.Commands
{
	public class CreatePaymentMethodVnPayCommandHandler : IRequestHandler<CreatePaymentMethodVnPayCommand, string>
	{
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _context;
		public CreatePaymentMethodVnPayCommandHandler(IConfiguration configuration,
			IHttpContextAccessor context)
		{
			_configuration = configuration;
			_context = context;
		}
		public async Task<string> Handle(CreatePaymentMethodVnPayCommand request, CancellationToken cancellationToken)
		{
			try
			{
				var order = await ComsumeOrderCreatedMessage();
				if (order.Id != request.OrderId)
				{
					return "Không hợp lệ";
				}
				var timeZoneById = TimeZoneInfo.FindSystemTimeZoneById(_configuration["TimeZoneId"]!);
				var timeNow = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, timeZoneById);
				var tick = DateTime.Now.Ticks.ToString();
				var pay = new VnPayLibrary();
				var urlCallBack = _configuration["PaymentCallBack:ReturnUrl"];

				pay.AddRequestData("vnp_Version", _configuration["Vnpay:Version"]!);
				pay.AddRequestData("vnp_Command", _configuration["Vnpay:Command"]!);
				pay.AddRequestData("vnp_TmnCode", _configuration["Vnpay:TmnCode"]!);
				pay.AddRequestData("vnp_Amount", (order.Amount * 100).ToString());
				pay.AddRequestData("vnp_CreateDate", timeNow.ToString("yyyyMMddHHmmss"));
				pay.AddRequestData("vnp_CurrCode", _configuration["Vnpay:CurrCode"]!);
				pay.AddRequestData("vnp_IpAddr", pay.GetIpAddress(_context.HttpContext!));
				pay.AddRequestData("vnp_Locale", _configuration["Vnpay:Locale"]!);
				pay.AddRequestData("vnp_OrderInfo", $"{order.Id}-{order.OrderDescription}-{order.Amount}");
				pay.AddRequestData("vnp_OrderType", "190000");
				pay.AddRequestData("vnp_ReturnUrl", urlCallBack!);
				pay.AddRequestData("vnp_TxnRef", $"{order.Id}");

				var paymentUrl = pay.CreateRequestUrl(_configuration["Vnpay:BaseUrl"]!, _configuration["Vnpay:HashSecret"]!);

				return paymentUrl;
			}
			catch (Exception)
			{
				throw;
			}
		}

		private async Task<OrderDto> ComsumeOrderCreatedMessage()
		{
			const string QueueName = "order_created";
			var factory = new ConnectionFactory()
			{
				HostName = "localhost",
				UserName = "guest",
				Password = "guest"
			};

			using var connection = factory.CreateConnection();
			using var channel = connection.CreateModel();
			var arguments = new Dictionary<string, object>
			{
				{ "x-message-ttl", 60000 } // TTL 60 seconds (matching the producer configuration)
			};

			//var queue = channel.QueueDeclare(
			//	queue: QueueName,

			//	durable: false,
			//	exclusive: false,
			//	autoDelete: false,
			//	arguments: arguments
			//);
			var queue = channel.QueueDeclarePassive(QueueName);
			var tcs = new TaskCompletionSource<OrderDto>();

			var consumer = new EventingBasicConsumer(channel);
			consumer.Received += (sender, eventArgs) =>
			{
				try
				{
					var messageBody = eventArgs.Body.ToArray();
					var orderDto = OrderDto.FromBytes(messageBody);

					channel.BasicAck(eventArgs.DeliveryTag, multiple: false);

					tcs.SetResult(orderDto);
				}
				catch (Exception ex)
				{
					tcs.SetException(ex);
				}
			};

			channel.BasicConsume(
				queue: queue.QueueName,
				autoAck: true,
				consumer: consumer
			);
			return await tcs.Task;
		}
	}
}
