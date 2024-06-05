using AutoMapper;
using MediatR;
using Shop.Domain.Entities;
using Shop.Domain.Repositories;

namespace Shop.Application.OrderStatuses.Commands.CreateOrderStatus
{
    public class CreateOrderStatusCommandHandler(IOrderStatusesRepository orderStatusesRepository, IMapper mapper) : IRequestHandler<CreateOrderStatusCommand, int>
    {
        public async Task<int> Handle(CreateOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var orderStatus = mapper.Map<OrderStatus>(request);
            int id = await orderStatusesRepository.Create(orderStatus);
            return id;
        }
    }
}
