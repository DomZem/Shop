using AutoMapper;
using MediatR;
using Shop.Application.ProductCategories.Dtos;
using Shop.Domain.Repositories;

namespace Shop.Application.ProductCategories.Queries.GetAllProductCategories
{
    public class GetAllProductCategoriesQueryHandler(IProductCategoriesRepository productCategoriesRepository, IMapper mapper) : IRequestHandler<GetAllProductCategoriesQuery, IEnumerable<ProductCategoryDto>>
    {
        public async Task<IEnumerable<ProductCategoryDto>> Handle(GetAllProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var productCategories = await productCategoriesRepository.GetAllAsync();
            var productCategoriesDtos = mapper.Map<IEnumerable<ProductCategoryDto>>(productCategories);
            return productCategoriesDtos;
        }
    }
}
