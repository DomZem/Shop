using MediatR;

namespace Shop.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommand : IRequest<int>
    {
        public string Name { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }
    }
}
