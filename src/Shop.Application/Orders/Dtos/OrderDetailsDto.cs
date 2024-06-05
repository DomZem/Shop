using Shop.Application.OrderStatuses.Dtos;
using Shop.Application.Products.Dtos;
using Shop.Application.Users.Dtos;

namespace Shop.Application.Orders.Dtos
{
    public class OrderDetailsDto
    {
        public int Id { get; set; }

        public DateTime OrderedAt { get; set; } = DateTime.UtcNow;

        public int ProductQuantity { get; set; }

        public ProductDto Product { get; set; } = default!;

        public OrderStatusDto OrderStatus { get; set; } = default!;

        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;

        public string PostalCode { get; set; } = default!;

        public string Country { get; set; } = default!;

        public decimal TotalPrice { get; set; }

        public UserDto OrderedBy { get; set; } = default!;
    }
}
