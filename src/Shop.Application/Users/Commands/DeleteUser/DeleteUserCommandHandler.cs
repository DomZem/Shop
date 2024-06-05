using MediatR;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommandHandler(IUsersRepository usersRepository) : IRequestHandler<DeleteUserCommand>
    {
        public async Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("User", request.Id.ToString());
            await usersRepository.Delete(user);
        }
    }
}
