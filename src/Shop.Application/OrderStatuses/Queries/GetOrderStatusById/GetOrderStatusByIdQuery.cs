using MediatR;
using Shop.Application.OrderStatuses.Dtos;

namespace Shop.Application.OrderStatuses.Queries.GetOrderStatusById
{
    public class GetOrderStatusByIdQuery : IRequest<OrderStatusDto>
    {
        public int Id { get; init; }
    }
}
