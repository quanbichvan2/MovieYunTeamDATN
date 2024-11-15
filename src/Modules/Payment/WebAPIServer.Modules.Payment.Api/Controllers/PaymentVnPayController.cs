/*using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.Payment.Businesses.HandlePaymentVnPay.Commands;
using WebAPIServer.Modules.Payment.Businesses.HandlePaymentVnPay.Models;
using WebAPIServer.Modules.Payment.Businesses.HandlePaymentVnPay.Queries;

namespace WebAPIServer.Modules.Payment.Api.Controllers
{
    internal class PaymentVnPayController : BaseController
    {
        private readonly IMediator _mediator;
        public PaymentVnPayController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //Payment Vnpay
        [HttpPost]
        public async Task<IActionResult> CreatePaymentUrl([FromBody] PaymentInformationModel model)
        {
            var command = new CreatePaymentVnPayCommand(model, HttpContext);
            var paymentUrl = await _mediator.Send(command);
            return Ok(paymentUrl);
        }
        [HttpGet]
        public async Task<IActionResult> PaymentCallback()
        {
            var query = new ExecutePaymentVnPayQuery(Request.Query);
            var response = await _mediator.Send(query);
            return new JsonResult(response);
        }
    }
}
*/