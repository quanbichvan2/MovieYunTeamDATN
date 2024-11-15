using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Commands;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleCombo.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Api.Controllers
{
    internal class CombosController : BaseController
    {
        private readonly IMediator _mediator;
        public CombosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCombo([FromQuery] Filter filter)
        {
            var categories = new GetAllCombosQuery(filter);
            var response = await _mediator.Send(categories);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetComboById(Guid id)
        {
            var category = new GetComboByIdQuery(id);
            var response = await _mediator.Send(category);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => NotFound(response.AsT1));
        }

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPost]
        public async Task<IActionResult> CreateCombo([FromBody] ComboForCreateDto model)
        {
            var combo = new CreateComboCommand(model);
            var response = await _mediator.Send(combo);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateCombo(Guid id, [FromBody] ComboForUpdateDto model)
        {
            var combo = new UpdateComboCommand(id, model);
            var response = await _mediator.Send(combo);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCombo(Guid id)
        {
            var command = new DeleteComboCommand(id);
            var response = await _mediator.Send(command);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }
    }
}
