using FluentValidation;

namespace Shop.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            

            RuleFor(dto => dto.Description)
                .NotEmpty().WithMessage("Description is required.");
        }
    }
}
