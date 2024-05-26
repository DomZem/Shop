using Shop.Application.OrderStatuses.Dtos;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Orders.Dtos
{
    public class OrderDto
    {
        public int Id { get; set; }

        public DateTime OrderedAt { get; set; } = DateTime.UtcNow;

        public ProductDto Product { get; set; } = default!;

        public int ProductQuantity { get; set; }

        public OrderStatusDto OrderStatus { get; set; } = default!;

        public decimal TotalPrice { get; set; }
    }
}
