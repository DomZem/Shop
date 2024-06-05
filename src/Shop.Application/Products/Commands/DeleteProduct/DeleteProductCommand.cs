using MediatR;

namespace Shop.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest
    {
        public int Id { get; init; } 
    }
}
