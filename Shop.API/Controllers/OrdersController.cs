using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shop.Application.Orders.Commands.CreateOrder;
using Shop.Application.Orders.Commands.DeleteOrder;
using Shop.Application.Orders.Commands.UpdateOrder;
using Shop.Application.Orders.Dtos;
using Shop.Application.Orders.Queries.GetAllOrders;
using Shop.Application.Orders.Queries.GetOrderById;
using Shop.Domain.Constants;

namespace Shop.API.Controllers
{
    [Route("api/orders")]
    [ApiController]
    public class OrdersController(IMediator mediator) : ControllerBase
    {
        [HttpGet]
        [Authorize(Roles = UserRoles.Admin)]
        public async Task<ActionResult<IEnumerable<OrderDto>>> GetAll()
        {
            var orders = await mediator.Send(new GetAllOrdersQuery());
            return Ok(orders);
        }

        [HttpGet("{id}")]
        [Authorize]    
        // TODO: Make it visible for owner
        public async Task<ActionResult<OrderDetailsDto?>> GetById([FromRoute] int id)
        {
            var order = await mediator.Send(new GetOrderByIdQuery() { Id = id });   
            return Ok(order);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateOrder(CreateOrderCommand command)
        {
            int id = await mediator.Send(command);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateOrder([FromRoute] int id, UpdateOrderCommand command)
        {
            command.Id = id;
            await mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = UserRoles.Admin)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteOrder([FromRoute] int id)
        {
            await mediator.Send(new DeleteOrderCommand() { Id = id });
            return NoContent();
        }
    }
}
