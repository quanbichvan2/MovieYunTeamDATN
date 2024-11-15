using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Commands;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleMovie.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
	internal class MoviesController : BaseController
	{
		private readonly IMediator _mediator;
		public MoviesController(IMediator mediator)
		{
			_mediator = mediator;
		}

		[HttpGet]
		public async Task<IActionResult> GetMovies([FromQuery] Filter filter)
		{
			var movie = new GetMoviesAllQuery(filter);
			var response = await _mediator.Send(movie);
			return Ok(response);
		}
		[HttpGet("scheduled")]
		public async Task<IActionResult> GetMoviesHaveShow([FromQuery] Filter filter, [FromQuery, DataType(DataType.Date)] DateTime selectedDate)
		{
			var movie = new GetMoviesHaveShowQuery(filter, selectedDate);
			var response = await _mediator.Send(movie);
			return Ok(response);
		}

		[HttpGet("{id}")]
		public async Task<IActionResult> GetMovie(Guid id)
		{
			var movie = new GetMovieByIdQuery(id);
			var response = await _mediator.Send(movie);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => NotFound(response.AsT1));
		}

		//[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPost]
		public async Task<IActionResult> CreateMovie([FromBody] MovieForCreateDto model)
		{
			var movie = new CreateMovieCommand(model);
			var response = await _mediator.Send(movie);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}

		//[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateMovie(Guid id, [FromBody] MovieForUpdateDto model)
		{
			var movie = new UpdateMovieCommand(model, id);
			var response = await _mediator.Send(movie);
			return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
		}
	}
}
