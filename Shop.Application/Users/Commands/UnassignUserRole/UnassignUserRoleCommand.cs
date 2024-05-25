using MediatR;

namespace Shop.Application.Users.Commands.UnassignUserRole
{
    public class UnassignUserRoleCommand : IRequest
    {
        public string UserEmail { get; set; } = default!;

        public string RoleName { get; set; } = default!;
    }
}
