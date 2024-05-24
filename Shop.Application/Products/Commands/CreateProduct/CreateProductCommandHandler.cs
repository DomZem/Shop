using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandHandler(IMapper mapper, IProductsRepository productsRepository) : IRequestHandler<CreateProductCommand, int>
    {
        public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var product = mapper.Map<Product>(request);
            int id = await productsRepository.Create(product);
            return id;
        }
    }
}
