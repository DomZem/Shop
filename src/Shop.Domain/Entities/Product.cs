namespace Shop.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public int ProductCategoryId { get; set; }

        public ProductCategory ProductCategory { get; set; } = default!;
    }
}
