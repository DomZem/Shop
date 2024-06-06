using FluentValidation;

namespace Shop.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
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
