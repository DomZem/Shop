using AutoMapper;
using MediatR;
using Shop.Application.Orders.Dtos;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQueryHandler(IOrdersRepository ordersRepository, IMapper mapper) : IRequestHandler<GetOrderByIdQuery, OrderDetailsDto>
    {
        public async Task<OrderDetailsDto> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            var order = await ordersRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Order", request.Id.ToString());
            var orderDto = mapper.Map<OrderDetailsDto>(order);
            return orderDto;
        }
    }
}
