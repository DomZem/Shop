namespace Shop.Domain.Entities
{
    public class OrderAddress
    {
        public string Street { get; set; } = default!;

        public string City { get; set; } = default!;

        public string PostalCode { get; set; } = default!;

        public string Country { get; set; } = default!;
    }
}
