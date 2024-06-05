using AutoMapper;
using MediatR;
using Shop.Domain.Repositories;

namespace Shop.Application.Users.Commands.CreateUser
{
    public class CreateUserCommandHandler(IUsersRepository usersRepository, IMapper mapper) : IRequestHandler<CreateUserCommand, string>
    {
        public async Task<string> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<Domain.Entities.User>(request);
            string id = await usersRepository.Create(user);
            return id;
        }
    }
}
