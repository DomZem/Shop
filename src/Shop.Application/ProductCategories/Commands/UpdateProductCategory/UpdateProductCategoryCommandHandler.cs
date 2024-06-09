using AutoMapper;
using MediatR;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommandHandler(IProductCategoriesRepository productCategoriesRepository, IMapper mapper) : IRequestHandler<UpdateProductCategoryCommand>
    {
        public async Task Handle(UpdateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var productCategory = await productCategoriesRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Product category", request.Id.ToString());
            mapper.Map(request, productCategory);
            await productCategoriesRepository.SaveChanges();
        }
    }
}
