using FluentValidation;

namespace Shop.Application.Products.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 25);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.");

            RuleFor(dto => dto.Price)
                .GreaterThan(0);

            RuleFor(dto => dto.Quantity)
                .GreaterThan(0);
            // Rest the rules 
        }
    }
}
