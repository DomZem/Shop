using MediatR;

namespace Shop.Application.OrderStatuses.Commands.CreateOrderStatus
{
    public class CreateOrderStatusCommand : IRequest<int>
    {
        public string Name { get; set; } = default!;
    }
}
