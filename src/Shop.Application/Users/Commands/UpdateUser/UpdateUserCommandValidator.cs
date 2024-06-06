using FluentValidation;

namespace Shop.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
    {
        public UpdateUserCommandValidator()
        {
            RuleFor(dto => dto.UserName)
                .Length(3, 50);

            RuleFor(dto => dto.Email)
                .EmailAddress()
                .WithMessage("Please provide a valid email address");

            RuleFor(dto => dto.PhoneNumber)
                .NotEmpty();
        }
    }
}
