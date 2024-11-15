using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Commands;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Queries;

namespace WebAPIServer.Modules.Payment.Api.Controllers
{
	internal class PaymentController : BaseController
	{
		private readonly IMediator _mediator;
		public PaymentController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpPost("vnpay")]
		public async Task<IActionResult> VnPayPayment([FromQuery] Guid orderId)
		{
			var command = new CreatePaymentMethodVnPayCommand(orderId);
			var paymentUrl = await _mediator.Send(command);
			return Ok(new { paymentUrl });
		}
		[HttpGet("vnpay")]
		public async Task<IActionResult> CallBackVnPay()
		{
			var query = new CallBackVnPayQuery(Request.Query);
			var response = await _mediator.Send(query);
			return Ok(Task.FromResult(response));
		}
		[HttpPost("cash")]
		public async Task<IActionResult> CashPayment()
		{
			/*var command = new CreatePaymentMethodVnPayCommand(orderId);
			var paymentUrl = await _mediator.Send(command);*/
			return Ok(await Task.FromResult("Phương thức này áp dụng sau ạ"));
		}
	}
}
