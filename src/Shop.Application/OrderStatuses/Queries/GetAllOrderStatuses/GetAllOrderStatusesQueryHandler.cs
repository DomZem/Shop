using AutoMapper;
using MediatR;
using Shop.Application.OrderStatuses.Dtos;
using Shop.Domain.Repositories;

namespace Shop.Application.OrderStatuses.Queries.GetAllOrderStatuses
{
    public class GetAllOrderStatusesQueryHandler(IOrderStatusesRepository orderStatusesRepository, IMapper mapper) : IRequestHandler<GetAllOrderStatusesQuery, IEnumerable<OrderStatusDto>>
    {
        public async Task<IEnumerable<OrderStatusDto>> Handle(GetAllOrderStatusesQuery request, CancellationToken cancellationToken)
        {
            var orderStatuses = await orderStatusesRepository.GetAllAsync();
            var orderStatusesDtos = mapper.Map<IEnumerable<OrderStatusDto>>(orderStatuses);
            return orderStatusesDtos;
        }
    }
}
