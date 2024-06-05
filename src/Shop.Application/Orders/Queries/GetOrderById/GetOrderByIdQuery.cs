using MediatR;
using Shop.Application.Orders.Dtos;

namespace Shop.Application.Orders.Queries.GetOrderById
{
    public class GetOrderByIdQuery : IRequest<OrderDetailsDto>
    {
        public int Id { get; init; }
    }
}
