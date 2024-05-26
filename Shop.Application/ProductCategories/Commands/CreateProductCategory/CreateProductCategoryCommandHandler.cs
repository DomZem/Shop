using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Application.ProductCategories.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandHandler(IProductCategoriesRepository productCategoriesRepository, IMapper mapper) : IRequestHandler<CreateProductCategoryCommand, int>
    {
        public async Task<int> Handle(CreateProductCategoryCommand request, CancellationToken cancellationToken)
        {
            var productCategory = mapper.Map<ProductCategory>(request);
            int id = await productCategoriesRepository.Create(productCategory);
            return id;
        }
    }
}
