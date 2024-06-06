using FluentValidation;

namespace Shop.Application.OrderStatuses.Commands.CreateOrderStatus
{
    public class CreateOrderStatusCommandValidator : AbstractValidator<CreateOrderStatusCommand>
    {
        public CreateOrderStatusCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 50);
        }
    }
}
