using MediatR;

namespace Shop.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommand : IRequest
    {
        public string? Id { get; set; }

        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string PhoneNumber { get; set; } = default!;
    }
}
