using Microsoft.AspNetCore.Mvc;
using Shop.Application.Products;
using Shop.Application.Products.Dtos;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class ProductsController(IProductsService productsService) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await productsService.GetAllProducts();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var product = await productsService.GetById(id);

            if(product is null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductDto createProductDto)
        {
            int id = await productsService.Create(createProductDto);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }
    }
}
