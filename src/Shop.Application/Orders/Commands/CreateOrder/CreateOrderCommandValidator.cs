using FluentValidation;

namespace Shop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(dto => dto.Street)
               .Length(3, 50);

            RuleFor(dto => dto.City)
                .Length(1, 100);
            
            RuleFor(dto => dto.PostalCode)
                .Matches(@"^\d{2}-\d{3}$")
                .WithMessage("Please provide a valid postal code (XX-XXX).");

            RuleFor(dto => dto.Country)
                 .Length(1, 100);

            RuleFor(dto => dto.ProductQuantity)
                .GreaterThan(0)
                .WithMessage("Product quantity must be grater than 0.");
        }
    }
}
