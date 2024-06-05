using MediatR;

namespace Shop.Application.ProductCategories.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommand : IRequest<int>
    {
        public string Name { get; set; }
    }
}
