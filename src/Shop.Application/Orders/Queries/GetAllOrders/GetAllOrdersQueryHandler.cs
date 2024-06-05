using AutoMapper;
using MediatR;
using Shop.Application.Orders.Dtos;
using Shop.Domain.Repositories;

namespace Shop.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQueryHandler(IOrdersRepository ordersRepository, IMapper mapper) : IRequestHandler<GetAllOrdersQuery, IEnumerable<OrderDto>>
    {
        public async Task<IEnumerable<OrderDto>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
        {
            var orders = await ordersRepository.GetAllAsync();
            var ordersDtos = mapper.Map<IEnumerable<OrderDto>>(orders);
            return ordersDtos;
        }
    }
}
