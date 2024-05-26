using MediatR;
using Shop.Application.OrderStatuses.Dtos;

namespace Shop.Application.OrderStatuses.Queries.GetAllOrderStatuses
{
    public class GetAllOrderStatusesQuery : IRequest<IEnumerable<OrderStatusDto>>
    {

    }
}
