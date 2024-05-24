using MediatR;

namespace Shop.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommand : IRequest<bool>
    {
        public int Id { get; init; } 
    }
}
