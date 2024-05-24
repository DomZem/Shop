using Shop.Application.Products.Dtos;

namespace Shop.Application.Products
{
    public interface IProductsService
    {
        Task<IEnumerable<ProductDto>> GetAllProducts();

        Task<ProductDto?> GetById(int id);

        Task<int> Create(CreateProductDto dto);
    }
}
