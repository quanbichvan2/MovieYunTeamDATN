using MediatR;
using Microsoft.AspNetCore.Mvc;
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

        [HttpGet]
        public async Task<IActionResult> GetSeatTypes()
        {
            var seatTypes = new GetSeatTypesAllQuery();
            var response = await _mediator.Send(seatTypes);
            return Ok(response);
        }
    }
}
