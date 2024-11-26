using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Commands;
using WebAPIServer.Modules.MovieManagement.Businesses.HandleSeatType.Queries;

namespace WebAPIServer.Modules.MovieManagement.Api.Controllers
{
    internal class SeatTypesController: BaseController
    {
        private readonly IMediator _mediator;
        public SeatTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateSeatTypeDto model)
        {
            var response = await _mediator.Send(new CreateSeatTypeCommand(model));
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateSeatTypeDto model)
        {
            var response = await _mediator.Send(new UpdateSeatTypeCommand(id, model));
            return response.Match<IActionResult>(
				_ => Ok(response.AsT0),
				error => BadRequest(response.AsT1));
        }

        [HttpGet]
        public async Task<IActionResult> GetSeatTypes()
        {
            var seatTypes = new GetSeatTypesAllQuery();
            var response = await _mediator.Send(seatTypes);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSeatTypeById(Guid id)
        {
            var seatType = new GetSeatTypeByIdQuery(id);
            var response = await _mediator.Send(seatType);
            return Ok(response);
        }
    }
}
