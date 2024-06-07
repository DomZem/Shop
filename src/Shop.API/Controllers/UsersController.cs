using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.Commands.AssignUserRole;
using Shop.Application.Users.Commands.CreateUser;
using Shop.Application.Users.Commands.DeleteUser;
using Shop.Application.Users.Commands.UnassignUserRole;
using Shop.Application.Users.Commands.UpdateUser;
using Shop.Application.Users.Dtos;
using Shop.Application.Users.Queries.GetAllUsers;
using Shop.Application.Users.Queries.GetUserById;
using Shop.Domain.Constants;

namespace Shop.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UsersController(IMediator mediator) : ControllerBase
    {
        [HttpPost("role")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("role")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto?>> GetById([FromRoute] string id)
        {
            var user = await mediator.Send(new GetUserByIdQuery() { Id = id });
            return Ok(user);
        }

        [HttpPost]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            string id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateUser([FromRoute] string id, UpdateUserCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteUser([FromRoute] string id)
        {
            await mediator.Send(new DeleteUserCommand() { Id = id });
            return NoContent();
        }
    }
}
