using MediatR;

namespace Shop.Application.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;
    }
}
