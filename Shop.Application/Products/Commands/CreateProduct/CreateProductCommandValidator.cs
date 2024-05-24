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

            // Rest the rules 
        }
    }
}
