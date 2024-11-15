using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Commands;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleGenre.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
	internal class GenresController : BaseController
	{
		private readonly IMediator _mediator;
		public GenresController(IMediator mediator)
		{
			_mediator = mediator;
		}
		[HttpGet]
		public async Task<IActionResult> GetGenres([FromQuery] Filter filter)
		{
			var genres = new GetGenresAllQuery(filter);
			var response = await _mediator.Send(genres);
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetGenre(Guid id)
		{
			var genre = new GetGenreByIdQuery(id);
			var response = await _mediator.Send(genre);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPost]
		public async Task<IActionResult> CreateGenre([FromBody] GenreForCreateDto model)
		{
			var genre = new CreateGenreCommand(model);
			var response = await _mediator.Send(genre);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateGenre(Guid id, [FromBody] GenreForUpdateDto model)
		{
			var genre = new UpdateGenreCommand(id, model);
			var response = await _mediator.Send(genre);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteGenre(Guid id)
		{
			var genre = new DeleteGenreCommand(id);
			var response = await _mediator.Send(genre);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}
	}
}