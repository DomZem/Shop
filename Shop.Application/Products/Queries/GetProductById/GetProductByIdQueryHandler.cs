using AutoMapper;
using MediatR;
using Shop.Application.Products.Dtos;
using Shop.Domain.Repositories;

namespace Shop.Application.Products.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<GetProductByIdQuery, ProductDto?>
    {
        public async Task<ProductDto?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id);
            var productDto = mapper.Map<ProductDto?>(product);
            return productDto;
        }
    }
}
