using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Commands;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Models;
using WebAPIServer.Modules.Booking.Businesses.HandleOrder.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Booking.Api.Controllers
{
	internal class OrdersController : BaseController
	{
		private readonly IMediator _mediator;
		public OrdersController(IMediator mediator)
		{
			_mediator = mediator;
		}

		//[Authorize(Roles = "ADMINISTRATOR")]
		[HttpGet]
		public async Task<IActionResult> GetOrders([FromQuery] Filter filter)
		{
			var orders = new GetOrdersAllQuery(filter);
			var response = await _mediator.Send(orders);
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetOrderById(Guid id)
		{
			var order = new GetOrderByIdQuery(id);
			var response = await _mediator.Send(order);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}

		//[Authorize]
		[HttpGet("GetOrderByUser")]
		public async Task<IActionResult> GetOrderByUser()
		{
			var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
			if (string.IsNullOrEmpty(userId))
			{
				return Unauthorized();
			}
			var order = new GetOrderByIdQuery(Guid.Parse(userId));
			var response = await _mediator.Send(order);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}

		//[Authorize]
		[HttpPost]
		public async Task<IActionResult> CreateOrder([FromBody] OrderForCreateDto model)
		{
			var order = new CreateOrderCommand(model);
			var response = await _mediator.Send(order);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}
    }
}
