using FluentValidation;

namespace Shop.Application.ProductCategories.Commands.UpdateProductCategory
{
    public class UpdateProductCategoryCommandValidator : AbstractValidator<UpdateProductCategoryCommand>
    {
        public UpdateProductCategoryCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 50);
        }
    }
}
