namespace Shop.Domain.Entities
{
    public class OrderStatus
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public List<Order> Orders { get; set; } = new();
    }
}
