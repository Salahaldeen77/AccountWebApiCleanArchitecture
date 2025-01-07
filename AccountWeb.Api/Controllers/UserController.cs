using AccountWeb.Api.Base;
using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Core.Features.Users.Queries.Models;
using AccountWeb.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountWeb.Api.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]//Access to the Class Private Except Admin or User

    public class UserController : AppControllerBase
    {
        [Authorize(Policy = "CreateUser")]
        [HttpPost(Router.UserRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddUserCommand command)
        {
            var response = await Mediator.Send(command);
            //return Ok(response);

            return NewResult(response);
        }

        [HttpGet(Router.UserRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetUserPaginationQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet(Router.UserRouting.GetById)]
        public async Task<IActionResult> GetUserById([FromRoute] int Id)
        {
            return NewResult(await Mediator.Send(new GetUserByIdQuery(Id)));
        }

        [Authorize(Policy = "EditUser")]
        [HttpPut(Router.UserRouting.Edite)]
        public async Task<IActionResult> Edite([FromBody] EditeUserCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

        [Authorize(Policy = "DeleteUser")]
        [HttpDelete(Router.UserRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            //var response =await Mediator.Send(command);
            //return Ok(response);
            return NewResult(await Mediator.Send(new DeleteUserCommand(Id)));
        }

        [HttpPut(Router.UserRouting.ChangePassword)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangeUserPasswordCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
