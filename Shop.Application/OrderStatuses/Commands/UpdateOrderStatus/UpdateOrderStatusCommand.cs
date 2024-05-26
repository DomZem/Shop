using MediatR;

namespace Shop.Application.OrderStatuses.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommand : IRequest
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

    }
}
