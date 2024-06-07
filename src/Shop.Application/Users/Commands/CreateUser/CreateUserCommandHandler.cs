using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Shop.Domain.Constants;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper, UserManager<Domain.Entities.User> userManager) : IRequestHandler<CreateUserCommand, string>
    {
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
            {
                throw new ExistingUserException(request.Email);
            }

            var user = mapper.Map<Domain.Entities.User>(request);
            string id = await usersRepository.Create(user);
            await userManager.AddToRoleAsync(user, UserRoles.User);
            return id;
        }
    }
}
