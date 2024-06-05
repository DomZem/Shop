namespace Shop.Domain.Entities
{
    public class ProductCategory
    {
        public int Id { get; set; }

        public string Name { get; set; } = default!;

        public List<Product> Products { get; set; } = new();
    }
}
