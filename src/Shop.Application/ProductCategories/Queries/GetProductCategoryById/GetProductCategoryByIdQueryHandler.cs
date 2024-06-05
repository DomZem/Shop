using AutoMapper;
using MediatR;
using Shop.Application.ProductCategories.Dtos;
using Shop.Application.Products.Dtos;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.ProductCategories.Queries.GetProductCategoryById
{
    public class GetProductCategoryByIdQueryHandler(IProductCategoriesRepository productCategoriesRepository, IMapper mapper) : IRequestHandler<GetProductCategoryByIdQuery, ProductCategoryDto>
    {
        public async Task<ProductCategoryDto> Handle(GetProductCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var productCategory = await productCategoriesRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Product category", request.Id.ToString());
            var productCategoryDto = mapper.Map<ProductCategoryDto>(productCategory);
            return productCategoryDto;
        }
    }
}
