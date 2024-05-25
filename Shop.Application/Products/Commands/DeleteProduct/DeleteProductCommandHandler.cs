using MediatR;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductsRepository productsRepository) : IRequestHandler<DeleteProductCommand>
    {
        public async Task Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id);
            if(product is null)
            {
                throw new NotFoundException("Product", request.Id.ToString());
            }

            await productsRepository.Delete(product);
        }
    }
}
