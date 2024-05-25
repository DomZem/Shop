using AutoMapper;
using MediatR;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand>
    {
        public async Task Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id);
            if (product is null)
            {
                throw new NotFoundException("Product", request.Id.ToString());
            }

            mapper.Map(request, product);
            await productsRepository.SaveChanges();
        }
    }
}
