using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Commands;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Models;
using WebAPIServer.Modules.Catalog.Businesses.HandleProduct.Queries;
using WebAPIServer.Shared.Abstractions.Models;

namespace WebAPIServer.Modules.Catalog.Api.Controllers
{
    internal class ProductsController : BaseController
    {
        private readonly IMediator _mediator;
        public ProductsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] Filter filter)
        {
            var products = new GetAllProductsQuery(filter);
            var response = await _mediator.Send(products);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = new GetProductByIdQuery(id);
            var response = await _mediator.Send(product);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => NotFound(response.AsT1));
        }

		//[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductForCreateDto model)
        {
            var product = new CreateProductCommand(model);
            var response = await _mediator.Send(product);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

		//[Authorize(Roles = "ADMINISTRATOR")]
		[HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Guid id, [FromBody] ProductForUpdateDto model)
        {
            var product = new UpdateProductCommand(id, model);
            var response = await _mediator.Send(product);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }

		[Authorize(Roles = "ADMINISTRATOR")]
		[HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(Guid id)
        {
            var command = new DeleteProductCommand(id);
            var response = await _mediator.Send(command);
            return response.Match<IActionResult>(
                _ => Ok(response.AsT0),
                error => BadRequest(response.AsT1));
        }
    }
}
