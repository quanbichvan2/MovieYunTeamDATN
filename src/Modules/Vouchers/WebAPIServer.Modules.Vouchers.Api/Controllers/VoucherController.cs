using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Commands;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Models;
using WebAPIServer.Modules.Vouchers.Businesses.HandleVoucher.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Vouchers.Api.Controllers
{
	internal class VoucherController : BaseController
	{
		private readonly IMediator _mediator;
		public VoucherController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetProduct([FromQuery] Filter filter)
		{
			var vouchers = new GetAllVoucherQuery(filter);
			var response = await _mediator.Send(vouchers);
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var voucher = new GetVoucherByIdQuery(id);
			var response = await _mediator.Send(voucher);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[HttpPost]
		public async Task<IActionResult> Post([FromBody] VoucherForCreateDto model)
		{
			var voucher = new CreateVoucherCommand(model);
			var response = await _mediator.Send(voucher);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> Put(Guid id, [FromBody] VoucherForUpdateDto model)
		{
			var voucher = new UpdateVoucherCommand(id, model);
			var response = await _mediator.Send(voucher);
			return response.Match<IActionResult>(
			_ => Ok(response.AsT0),
			error => NotFound(response.AsT1));
		}

		[HttpDelete("{id}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			var voucher = new DeleteVoucherCommand(id);
			var response = await _mediator.Send(voucher);
			return response.Match<IActionResult>(
			_ => Ok(response.AsT0),
			error => NotFound(response.AsT1));
		}
	}
}
