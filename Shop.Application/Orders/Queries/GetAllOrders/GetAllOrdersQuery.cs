using MediatR;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.Queries.GetAllOrders
{
    public class GetAllOrdersQuery : IRequest<IEnumerable<OrderDto>>
    {

    }
}
