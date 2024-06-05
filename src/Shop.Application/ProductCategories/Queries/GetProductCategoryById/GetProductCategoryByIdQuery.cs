using MediatR;
using Shop.Application.ProductCategories.Dtos;

namespace Shop.Application.ProductCategories.Queries.GetProductCategoryById
{
    public class GetProductCategoryByIdQuery : IRequest<ProductCategoryDto>
    {
        public int Id { get; init; }
    }
}
