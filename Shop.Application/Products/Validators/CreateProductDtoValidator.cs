using FluentValidation;
using Shop.Application.Products.Dtos;

namespace Shop.Application.Products.Validators
{
    public class CreateProductDtoValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductDtoValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 25);

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.");

            // Rest the rules 
        }
    }
}
