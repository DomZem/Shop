using AutoMapper;
using MediatR;
using Shop.Application.Products.Dtos;
using Shop.Domain.Repositories;

namespace Shop.Application.Products.Queries.GetAllProducts
{
    public class GetAllProductsQueryHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
    {
        public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await productsRepository.GetAllAsync();
            var productsDtos = mapper.Map<IEnumerable<ProductDto>>(products);
            return productsDtos;
        }
    }
}
