using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication.Core.Users.Commands;
using WebApplication.Core.Users.Common.Models;
using WebApplication.Core.Users.Queries;

namespace WebApplication.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUserAsync(
            [FromQuery] GetUserQuery query,
            CancellationToken cancellationToken)
        {
            UserDto result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        [HttpGet("Find")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync(
     [FromQuery] FindUsersQuery query,
     CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }

        // TODO: create a route (at /Find) that can retrieve a list of matching users using the `FindUsersQuery`

        [HttpGet("List")]
        [ProducesResponseType(typeof(IEnumerable<UserDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetUsersAsync(
                     [FromQuery] ListUsersQuery query,
                        CancellationToken cancellationToken)
        {
           
            var result = await _mediator.Send(query, cancellationToken);
            return Ok(result);
        }


        // TODO: create a route (at /List) that can retrieve a paginated list of users using the `ListUsersQuery`

        [HttpPost]
        public async Task<ActionResult> Create(CreateUserCommand createUserCommand,
            CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(createUserCommand, cancellationToken));
               
        }
        // TODO: create a route that can create a user using the `CreateUserCommand`
        [HttpPut]
        public async Task<IActionResult> Update(UpdateUserCommand updateUserCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(updateUserCommand, cancellationToken));
        }
        // TODO: create a route that can update an existing user using the `UpdateUserCommand`
        [HttpDelete]
        public async Task<IActionResult> Delete(DeleteUserCommand deleteUserCommand, CancellationToken cancellationToken)
        {
            return Ok(await _mediator.Send(deleteUserCommand,cancellationToken));
        }
        // TODO: create a route that can delete an existing user using the `DeleteUserCommand`
    }
}
