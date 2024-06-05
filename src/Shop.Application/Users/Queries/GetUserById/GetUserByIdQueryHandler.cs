using AutoMapper;
using MediatR;
using Shop.Application.Users.Dtos;
using Shop.Domain.Exceptions;
using Shop.Domain.Repositories;

namespace Shop.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQueryHandler(IUsersRepository usersRepository, IMapper mapper) : IRequestHandler<GetUserByIdQuery, UserDto>
    {
        public async Task<UserDto> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await usersRepository.GetByIdAsync(request.Id) ?? throw new NotFoundException("User", request.Id.ToString());
            var userDto = mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
