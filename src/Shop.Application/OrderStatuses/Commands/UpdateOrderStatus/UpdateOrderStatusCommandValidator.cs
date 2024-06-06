using FluentValidation;

namespace Shop.Application.OrderStatuses.Commands.UpdateOrderStatus
{
    public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
    {
        public UpdateOrderStatusCommandValidator()
        {
            RuleFor(dto => dto.Name)
                .Length(3, 50);
        }
    }
}
