using FluentValidation;

namespace Shop.Application.Orders.Commands.CreateOrder
{
    public class CreateOrderCommandValidator : AbstractValidator<CreateOrderCommand>
    {
        public CreateOrderCommandValidator()
        {
            RuleFor(dto => dto.Street)
               .Length(3, 25);

            // TODO: Add more validation
        }
    }
}
