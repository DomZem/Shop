using MediatR;

namespace Shop.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; init; }
    }
}
