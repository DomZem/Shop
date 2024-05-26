using MediatR;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler(IOrdersRepository ordersRepository) : IRequestHandler<DeleteOrderCommand>
    {
        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await ordersRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Order", request.Id.ToString());
            await ordersRepository.Delete(order);
        }
    }
}
