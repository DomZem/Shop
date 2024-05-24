using MediatR;
using Shop.Domain.Repositories;

namespace Shop.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler(IProductsRepository productsRepository) : IRequestHandler<DeleteProductCommand, bool>
    {
        public async Task<bool> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id);
            if(product is null)
            {
                return false;
            }

            await productsRepository.Delete(product);
            return true;
        }
    }
}
