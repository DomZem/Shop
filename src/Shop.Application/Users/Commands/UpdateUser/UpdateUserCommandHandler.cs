using AutoMapper;
using MediatR;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Users.Commands.UpdateUser
{
    public class UpdateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper) : IRequestHandler<UpdateUserCommand>
    {
        public async Task Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("User", request.Id.ToString());
            mapper.Map(request, user);
            await usersRepository.SaveChanges();
        }
    }
}
