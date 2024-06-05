using Shop.Application.ProductCategories.Dtos;

namespace Shop.Application.Products.Dtos
{
    public class ProductDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int Quantity { get; set; }

        public decimal Price { get; set; }

        public ProductCategoryDto ProductCategory { get; set; } = default!;
    }
}
