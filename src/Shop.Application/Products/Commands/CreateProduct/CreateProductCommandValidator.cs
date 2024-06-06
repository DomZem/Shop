using FluentValidation;

namespace Shop.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 50);

            RuleFor(dto => dto.Description)
                .NotEmpty()
                .WithMessage("Description is required.");

            RuleFor(dto => dto.Price)
                .GreaterThan(0)
                .WithMessage("Price must be a non-negative number.");

            RuleFor(dto => dto.Quantity)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Quantity must be a non-negative number.");
        }
    }
}
