using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Commands;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Models;
using WebAPIServer.Modules.Tickets.Businesses.HanldeTicketType.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Tickets.Api.Controllers
{

	internal class TicketTypesController : BaseController
    {
        private readonly IMediator _mediator;
        public TicketTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
		public async Task<IActionResult> GetTicketTypes([FromQuery] Filter filter)
        {
            var ticketTypes = new GetAllTicketTypesQuery(filter);
            var response = await _mediator.Send(ticketTypes);
            return Ok(response);
        }

        [HttpGet("{id}")]
		public async Task<IActionResult> GetTicketTypeById(Guid id)
        {
            var ticketType = new GetTicketTypeByIdQuery(id);
            var response = await _mediator.Send(ticketType);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => NotFound(response.AsT1));
        }

        [HttpPost]
		public async Task<IActionResult> CreateTicketType([FromBody] TicketTypeForCreateDto model)
        {
            var ticketType = new CreateTicketTypeCommand(model);
            var response = await _mediator.Send(ticketType);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

        [HttpPut("{id}")]
		public async Task<IActionResult> UpdateTicketType(Guid id, [FromBody] TicketTypeForUpdateDto model)
        {
            var ticketType = new UpdateTicketTypeCommand(id, model);
            var response = await _mediator.Send(ticketType);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

        [HttpDelete("{id}")]
		public async Task<IActionResult> DeleteTicketType(Guid id)
        {
            var ticketType = new DeleteTicketTypeCommand(id);
            var response = await _mediator.Send(ticketType);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }
    }
}
