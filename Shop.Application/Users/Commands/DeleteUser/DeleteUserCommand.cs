﻿using MediatR;

namespace Shop.Application.Users.Commands.DeleteUser
{
    public class DeleteUserCommand : IRequest
    {
        public string Id { get; init; }

    }
}
