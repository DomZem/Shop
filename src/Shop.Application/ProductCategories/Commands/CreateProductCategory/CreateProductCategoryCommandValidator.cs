using FluentValidation;

namespace Shop.Application.ProductCategories.Commands.CreateProductCategory
{
    public class CreateProductCategoryCommandValidator : AbstractValidator<CreateProductCategoryCommand>
    {
        public CreateProductCategoryCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 50);
        }
    }
}
