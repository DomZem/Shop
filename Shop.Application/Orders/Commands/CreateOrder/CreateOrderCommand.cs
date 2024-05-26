using MediatR;

namespace Shop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommand : IRequest<int>
    {
        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;

        public string PostalCode { get; set; } = default!;

        public string Country { get; set; } = default!;

        public int ProductQuantity { get; set; }

        public int ProductId { get; set; }

        public int OrderStatusId { get; set; }

        public string OrderedById { get; set; } = default!;
    }
}
