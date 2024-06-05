using MediatR;
using Shop.Application.ProductCategories.Dtos;

namespace Shop.Application.ProductCategories.Queries.GetAllProductCategories
{
    public class GetAllProductCategoriesQuery : IRequest<IEnumerable<ProductCategoryDto>>
    {

    }
}
