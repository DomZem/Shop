using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Users.Commands.AssignUserRole;
using Shop.Application.Users.Commands.UnassignUserRole;
using Shop.Application.Users.Dtos;
using Shop.Application.Users.Queries.GetAllUsers;
using Shop.Domain.Constants;

namespace Shop.API.Controllers
{
    [Route("api/identity")]
    [ApiController]
    public class IdentityController(IMediator mediator) : ControllerBase
    {
        [HttpPost("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> AssignUserRole(AssignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("userRole")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<IActionResult> UnassignUserRole(UnassignUserRoleCommand command)
        {
            await mediator.Send(command);
            return NoContent();
        }

        [HttpGet("users")]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = await mediator.Send(new GetAllUsersQuery());
            return Ok(users);
        }
    }
}
