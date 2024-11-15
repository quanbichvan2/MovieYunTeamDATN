using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Commands;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleHall.Queries;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
	internal class HallsController : BaseController
	{
		private readonly IMediator _mediator;
		public HallsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetHalls()
		{
			var halls = new GetHallsAllQuery();
			var response = await _mediator.Send(halls);
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetHall(Guid id)
		{
			var hall = new GetHallByIdQuery(id);
			var response = await _mediator.Send(hall);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}

		//[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPost]
		public async Task<IActionResult> CreateHall([FromBody] HallForCreateDto model)
		{
			var hall = new CreateHallCommand(model);
			var response = await _mediator.Send(hall);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		//[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateHall(Guid id, [FromBody] HallForUpdateDto model)
		{
			var hall = new UpdateHallCommand(id, model);
			var response = await _mediator.Send(hall);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}
	}
}
