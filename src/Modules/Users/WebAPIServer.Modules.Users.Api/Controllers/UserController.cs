using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Commands;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Models;
using WebAPIServer.Modules.Users.Businesses.HandleUser.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Users.Api.Controllers
{
	internal class UserController : BaseController
	{
		private readonly IMediator _mediator;
		public UserController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetUsers([FromQuery] Filter filter)
		{
			var products = new GetAllUsersQuery(filter);
			var response = await _mediator.Send(products);
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(Guid id)
		{
			var product = new GetUserByIdQuery(id);
			var response = await _mediator.Send(product);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}

		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] UserForCreateDto model)
		{
			var product = new CreateUserCommand(model);
			var response = await _mediator.Send(product);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UserForUpdateDto model)
		{
			var product = new UpdateUserCommand(id, model);
			var response = await _mediator.Send(product);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[NonAction]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(Guid id)
		{
			var command = new DeleteUserCommand(id);
			var response = await _mediator.Send(command);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}
	}
}
