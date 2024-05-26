using AutoMapper;

namespace Shop.Application.Users.Dtos
{
    public class UsersProfile : Profile
    {
        public UsersProfile()
        {
            CreateMap<Domain.Entities.User, UserDto>(); 
        }
    }
}
