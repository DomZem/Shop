using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandHandler(IOrdersRepository ordersRepository, IMapper mapper) : IRequestHandler<CreateOrderCommand, int>
    {
        public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
        {
            var order = mapper.Map<Order>(request);
            int id = await ordersRepository.Create(order);
            return id;
        }
    }
}
