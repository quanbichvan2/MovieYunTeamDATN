using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Commands;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Models;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleShow.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
	internal class ShowsController : BaseController
    {
        private readonly IMediator _mediator;
        public ShowsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetShows([FromQuery] Filter filter)
        {
            var show = new GetShowsAllQuery(filter);
            var response = await _mediator.Send(show);
            return Ok(response);
        }

        [HttpGet("GetShowByMovieId/{id}")]
        public async Task<IActionResult> GetShowByMovieId(Guid id)
        {
            var show = new GetShowByMovieIdQuery(id);
            var response = await _mediator.Send(show);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => NotFound(response.AsT1));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetShow(Guid id)
        {
            var show = new GetShowByIdQuery(id);
            var response = await _mediator.Send(show);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => NotFound(response.AsT1));
        }

        //[Authorize(Roles = "ADMINISTRATOR")]
        [HttpPost]
        public async Task<IActionResult> CreateShow([FromBody] ShowForCreateDto model)
        {
            var show = new CreateShowCommand(model);
            var response = await _mediator.Send(show);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

		//[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateShow(Guid id, [FromBody] ShowForUpdateDto model)
        {
            var show = new UpdateShowCommand(model, id);
            var response = await _mediator.Send(show);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }
    }
}
