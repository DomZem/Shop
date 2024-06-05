using AutoMapper;
using Shop.Application.Users.Commands.CreateUser;
using Shop.Application.Users.Commands.UpdateUser;

namespace Shop.Application.Users.Dtos
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Domain.Entities.User, UserDto>();
            CreateMap<CreateUserCommand, Domain.Entities.User>();
            CreateMap<UpdateUserCommand, Domain.Entities.User>();
        }
    }
}
