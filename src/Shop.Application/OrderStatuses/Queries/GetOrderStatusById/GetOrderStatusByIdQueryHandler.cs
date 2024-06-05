using AutoMapper;
using MediatR;
using Shop.Application.OrderStatuses.Dtos;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.OrderStatuses.Queries.GetOrderStatusById
{
    public class GetOrderStatusByIdQueryHandler(IOrderStatusesRepository orderStatusesRepository, IMapper mapper) : IRequestHandler<GetOrderStatusByIdQuery, OrderStatusDto>
    {
        public async Task<OrderStatusDto> Handle(GetOrderStatusByIdQuery request, CancellationToken cancellationToken)
        {
            var orderStatus = await orderStatusesRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("Order status", request.Id.ToString());
            var orderStatusDto = mapper.Map<OrderStatusDto>(orderStatus);
            return orderStatusDto;
        }
    }
}
