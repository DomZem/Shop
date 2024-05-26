using AutoMapper;
using MediatR;
using Shop.Application.Users.Dtos;
using Shop.Domain.Repositories;

namespace Shop.Application.Users.Queries.GetAllUsers
{
    public class GetAllUsersQueryHandler(IUsersRepository usersRepository, IMapper mapper) : IRequestHandler<GetAllUsersQuery, IEnumerable<UserDto>>
    {
        public async Task<IEnumerable<UserDto>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            var users = await usersRepository.GetAllAsync();
            var usersDtos = mapper.Map<IEnumerable<UserDto>>(users);
            return usersDtos;
        }
    }
}
