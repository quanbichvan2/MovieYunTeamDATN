using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Commands;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleDirector.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
	internal class DirectorsController : BaseController
	{
		private readonly IMediator _mediator;
		public DirectorsController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetDirectors([FromQuery] Filter filter)
		{
			var directors = new GetDirectorsAllQuery(filter);
			var response = await _mediator.Send(directors);
			return Ok(response);
		}


		[HttpGet("{id}")]
		public async Task<IActionResult> GetDirector(Guid id)
		{
			var director = new GetDirectorByIdQuery(id);
			var response = await _mediator.Send(director);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPost]
		public async Task<IActionResult> CreateDirector([FromBody] DirectorForCreateDto model)
		{
			var director = new CreateDirectorCommand(model);
			var response = await _mediator.Send(director);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateDirector(Guid id, [FromBody] DirectorForUpdateDto model)
		{
			var director = new UpdateDirectorCommand(id, model);
			var response = await _mediator.Send(director);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteDirector(Guid id)
		{
			var director = new DeleteDirectorCommand(id);
			var response = await _mediator.Send(director);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}
	}
}
