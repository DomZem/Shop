using AutoMapper;
using MediatR;
using Shop.Domain.Repositories;

namespace Shop.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler(IProductsRepository productsRepository, IMapper mapper) : IRequestHandler<UpdateProductCommand, bool>
    {
        public async Task<bool> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await productsRepository.GetByIdAsync(request.Id);
            if (product is null)
            {
                return false;
            }

            mapper.Map(request, product);
            await productsRepository.SaveChanges();

            return true;
        }
    }
}
