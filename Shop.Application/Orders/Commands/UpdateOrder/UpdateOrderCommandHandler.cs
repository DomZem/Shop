using AutoMapper;
using MediatR;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Orders.Commands.UpdateOrder
{
    public class UpdateOrderCommandHandler(IOrdersRepository ordersRepository, IMapper mapper) : IRequestHandler<UpdateOrderCommand>
    {
        public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await ordersRepository.GetByIdAsync(request.Id);
            if (order is null)
            {
                throw new NotFoundException("Order", request.Id.ToString());
            }

            mapper.Map(request, order);
            await ordersRepository.SaveChanges();
        }
    }
}
