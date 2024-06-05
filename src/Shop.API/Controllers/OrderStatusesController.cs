using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.OrderStatuses.Commands.CreateOrderStatus;
using Shop.Application.OrderStatuses.Commands.UpdateOrderStatus;
using Shop.Application.OrderStatuses.Dtos;
using Shop.Application.OrderStatuses.Queries.GetAllOrderStatuses;
using Shop.Application.OrderStatuses.Queries.GetOrderStatusById;
using Shop.Domain.Constants;

namespace Shop.API.Controllers
{
    [ApiController]
    [Route("api/orderStatuses")]
    //[Authorize(Roles = UserRoles.Admin)]
    public class OrderStatusesController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusDto>>> GetAll()
        {
            var orderStatuses = await mediator.Send(new GetAllOrderStatusesQuery());
            return Ok(orderStatuses);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderStatusDto?>> GetById([FromRoute] int id)
        {
            var orderStatus = await mediator.Send(new GetOrderStatusByIdQuery() { Id = id });
            return Ok(orderStatus);
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrderStatus(CreateOrderStatusCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrderStatus([FromRoute] int id, UpdateOrderStatusCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }
    }
}
