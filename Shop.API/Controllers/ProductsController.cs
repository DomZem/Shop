using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products.Commands.CreateProduct;
using Shop.Application.Products.Commands.DeleteProduct;
using Shop.Application.Products.Commands.UpdateProduct;
using Shop.Application.Products.Dtos;
using Shop.Application.Products.Queries.GetAllProducts;
using Shop.Application.Products.Queries.GetProductById;
using Shop.Domain.Constants;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDto>>> GetAll()
        {
            var products = await mediator.Send(new GetAllProductsQuery());
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto?>> GetById([FromRoute] int id)
        {
            var product = await mediator.Send(new GetProductByIdQuery() { Id = id });
            return Ok(product);
        }

        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            await mediator.Send(new DeleteProductCommand() { Id = id });
            return NoContent();
        }

        [HttpPatch("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductCommand command)
        {
            command.Id = id;
            await mediator.Send(new UpdateProductCommand() { Id = id });
            return NoContent();
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {   
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
