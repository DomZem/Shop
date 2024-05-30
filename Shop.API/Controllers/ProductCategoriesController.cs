using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.ProductCategories.Commands.CreateProductCategory;
using Shop.Application.ProductCategories.Commands.UpdateProductCategory;
using Shop.Application.ProductCategories.Dtos;
using Shop.Application.ProductCategories.Queries.GetAllProductCategories;
using Shop.Application.ProductCategories.Queries.GetProductCategoryById;
using Shop.Domain.Constants;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/productCategories")]
    [Authorize(Roles = UserRoles.Admin)]
    public class ProductCategoriesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductCategoryDto>>> GetAll()
        {
            var productCategories = await mediator.Send(new GetAllProductCategoriesQuery());
            return Ok(productCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductCategoryDto?>> GetById([FromRoute] int id)
        {
            var productCategory = await mediator.Send(new GetProductCategoryByIdQuery() { Id = id });
            return Ok(productCategory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProductCategory(CreateProductCategoryCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateProductCategory([FromRoute] int id, UpdateProductCategoryCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
