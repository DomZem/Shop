using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Exceptions;

namespace Shop.Application.Users.Commands.UnassignUserRole
{
    public class UnassignUserRoleCommandHandler(UserManager<Domain.Entities.User> userManager, RoleManager<IdentityRole> roleManager) : IRequestHandler<UnassignUserRoleCommand>
    {
        public async Task Handle(UnassignUserRoleCommand request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.UserEmail) ?? throw new NotFoundException(nameof(Domain.Entities.User), request.UserEmail);

            var role = await roleManager.FindByNameAsync(request.RoleName) ?? throw new NotFoundException(nameof(IdentityRole), request.RoleName);

            await userManager.RemoveFromRoleAsync(user, role.Name!);
        }
    }
}
