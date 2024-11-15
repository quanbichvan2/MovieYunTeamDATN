using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Commands;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleCastMember.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
	internal class CastMembersController : BaseController
	{
		private readonly IMediator _mediator;
		public CastMembersController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetCastMembers([FromQuery] Filter filter)
		{
			var castMembers = new GetCastMembersAllQuery(filter);
			var response = await _mediator.Send(castMembers);
			return Ok(response);
		}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetCastMember(Guid id)
		{
			var castMember = new GetCastMemberByIdQuery(id);
			var response = await _mediator.Send(castMember);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}
		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPost]
		public async Task<IActionResult> CreateCastMember([FromBody] CastMemberForCreateDto model)
		{
			var castMember = new CreateCastMemberCommand(model);
			var response = await _mediator.Send(castMember);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateCastMember(Guid id, [FromBody] CastMemberForUpdateDto model)
		{
			var castMember = new UpdateCastMemberCommand(model, id);
			var response = await _mediator.Send(castMember);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCastMember(Guid id)
		{
			var castMember = new DeleteCastMemberCommand(id);
			var response = await _mediator.Send(castMember);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}
	}
}
