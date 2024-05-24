using AutoMapper;
using Shop.Application.Products.Dtos;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Application.Products
{
    internal class ProductsService(IProductsRepository productsRepository, IMapper mapper) : IProductsService
    {
        public async Task<int> Create(CreateProductDto dto)
        {
            var product = mapper.Map<Product>(dto);
            int id = await productsRepository.Create(product);
            return id;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await productsRepository.GetAllAsync();
            var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDtos;
        }

        public async Task<ProductDto?> GetById(int id)
        {
            var product = await productsRepository.GetByIdAsync(id);
            var productDto = mapper.Map<ProductDto>(product);
            return productDto;
        }
    }
}
