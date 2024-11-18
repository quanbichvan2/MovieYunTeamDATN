using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Stripe;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Commands;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Dtos;
using WebAPIServer.Modules.Payment.Businesses.HandlePayment.Queries;

namespace WebAPIServer.Modules.Payment.Api.Controllers
{
	internal class PaymentController : BaseController
	{
		private readonly IMediator _mediator;
        private readonly StripeSettings _stripeSettings;
        public PaymentController(IMediator mediator, IOptions<StripeSettings> stripeSettings)
		{
			_mediator = mediator;
            _stripeSettings = stripeSettings.Value;
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

        [HttpPost()]
        public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest model)
        {
            var command = new CreatePaymentCommand(model);
            var paymentUrl = await _mediator.Send(command);
            return Ok(new { paymentUrl });
        }

		[HttpPost("confirm-payment")]
		public async Task<IActionResult> ConfirmPayment(string paymentIntentId)
		{
			try
			{
                StripeConfiguration.ApiKey = _stripeSettings.SecretKey;
                var service = new PaymentIntentService();
                var paymentIntent = await service.GetAsync(paymentIntentId);

                return Ok(new
                {
                    Status = paymentIntent.Status,
                    Amount = paymentIntent.Amount,
                    Currency = paymentIntent.Currency,
                    CreatedAt = paymentIntent.Created
                });
            }
			catch (StripeException ex)
			{
				return BadRequest(new { error = ex.Message });
			}
		}
	}
}
