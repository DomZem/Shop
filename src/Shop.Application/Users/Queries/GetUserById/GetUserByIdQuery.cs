using MediatR;
using Shop.Application.Users.Dtos;

namespace Shop.Application.Users.Queries.GetUserById
{
    public class GetUserByIdQuery : IRequest<UserDto>
    {
        public string Id { get; init; }

    }
}
