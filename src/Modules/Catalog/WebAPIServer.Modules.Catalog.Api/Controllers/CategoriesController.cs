using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Commands;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleCategory.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Api.Controllers
{
	internal class CategoriesController : BaseController
    {
        private readonly IMediator _mediator;
        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] Filter filter)
        {
            var categories = new GetAllCategoriesQuery(filter);
            var response = await _mediator.Send(categories);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = new GetCategoryByIdQuery(id);
            var response = await _mediator.Send(category);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => NotFound(response.AsT1));
        }

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] CategoryForCreateDto model)
        {
            var category = new CreateCategoryCommand(model);
            var response = await _mediator.Send(category);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] CategoryForUpdateDto model)
        {
            var category = new UpdateCategoryCommand(id, model);
            var response = await _mediator.Send(category);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var command = new DeleteCategoryCommand(id);
            var response = await _mediator.Send(command);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }
    }
}
