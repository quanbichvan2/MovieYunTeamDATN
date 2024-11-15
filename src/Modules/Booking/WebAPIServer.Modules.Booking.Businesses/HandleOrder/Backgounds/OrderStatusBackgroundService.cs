using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using WebAPIServer.Modules.Booking.Businesses.Contracts;
using WebAPIServer.Modules.Booking.Domain.Entities;

namespace WebAPIServer.Modules.Booking.Businesses.HandleOrder.Backgounds
{
	public class OrderStatusBackgroundService : BackgroundService
	{
		private readonly IServiceScopeFactory _serviceScopeFactory;
		private IConnection _connection;
		private IModel _channel;
		public OrderStatusBackgroundService(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
			var factory = new ConnectionFactory() { HostName = "localhost", UserName = "guest", Password = "guest" };
			_connection = factory.CreateConnection();
			_channel = _connection.CreateModel();
			_channel.QueueDeclare(queue: "order_dead_letter_queue", durable: true, exclusive: false, autoDelete: false);
		}

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			while (!stoppingToken.IsCancellationRequested)
			{
				await CheckAndUpdateOrderStatuses();
				ReceiveOrderDeadLetterMessage();
				// Chờ 1 phút trước khi chạy lại
				await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
			}
		}
		private async Task CheckAndUpdateOrderStatuses()
		{
			using (var scope = _serviceScopeFactory.CreateScope())
			{
				var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
				var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

				// Thực hiện kiểm tra và cập nhật trạng thái đơn hàng

				var getPendingOrder = orderRepository.GetAll()
					.Where(x => x.OrderStatusId == OrderStatusConstants.Pending).ToList();
				var getComfirmedOrder = orderRepository.GetAll()
					.Where(x => x.OrderStatusId == OrderStatusConstants.Confirmed).ToList();
				// kiểm tra cho các đơn hàng đang pending
				foreach (var order in getPendingOrder)
				{
					if (DateTime.Now >= order.CreatedAt.AddMinutes(2))
					{
						// Nếu đã qua 15 phút, cập nhật trạng thái sang Canceled
						order.OrderStatusId = OrderStatusConstants.Canceled;
						order.OrderStatus = await orderRepository.GetStatusByIdAsync(OrderStatusConstants.Canceled);

						orderRepository.Update(order);
					}
				}

				// Kiểm tra cho các đơn hàng Confirmed
				foreach (var order in getComfirmedOrder)
				{
					if (DateTime.Now >= order.ShowEndAt && order.OrderStatusId != OrderStatusConstants.CheckedIn)
					{
						// Nếu đã qua giờ chiếu mà chưa CheckedIn
						order.OrderStatusId = OrderStatusConstants.Abandoned;
						order.OrderStatus = await orderRepository.GetStatusByIdAsync(OrderStatusConstants.Abandoned);
						orderRepository.Update(order);
					}
				}
				await unitOfWork.SaveChangesAsync();
			}
		}
		private void ReceiveOrderDeadLetterMessage()
		{
			var consumer = new EventingBasicConsumer(_channel);
			consumer.Received += async (model, ea) =>
			{
				var body = ea.Body.ToArray();
				var message = Encoding.UTF8.GetString(body);

				// Deserialize message to get order ID (assuming message contains the order ID)
				var order = JsonConvert.DeserializeObject<Order>(message);
				if (order != null)
				{
					// Create a new service scope for each message processing to get a fresh DbContext instance
					using (var scope = _serviceScopeFactory.CreateScope())
					{
						var orderRepository = scope.ServiceProvider.GetRequiredService<IOrderRepository>();
						var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

						// Fetch and update the order within the scope
						var orderToUpdate = orderRepository.GetAll()
							.FirstOrDefault(o => o.Id == order.Id && o.OrderStatusId == OrderStatusConstants.Requested);

						if (orderToUpdate != null)
						{
							orderToUpdate.OrderStatusId = OrderStatusConstants.Canceled;
							orderToUpdate.OrderStatus = await orderRepository.GetStatusByIdAsync(OrderStatusConstants.Canceled);
							orderRepository.Update(orderToUpdate);
							await unitOfWork.SaveChangesAsync();
						}
					}
				}

				// Acknowledge message after processing
				_channel.BasicAck(ea.DeliveryTag, false);
			};

			_channel.BasicConsume(queue: "order_dead_letter_queue", autoAck: false, consumer: consumer);
		}

		public override void Dispose()
		{
			_channel.Close();
			_connection.Close();
			base.Dispose();
		}
	}
}
