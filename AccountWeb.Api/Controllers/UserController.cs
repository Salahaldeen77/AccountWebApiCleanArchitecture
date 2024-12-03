using AccountWeb.Api.Base;
using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Core.Features.Users.Queries.Models;
using AccountWeb.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace AccountWeb.Api.Controllers
{
    // [Route("api/[controller]")]
    [ApiController]
    public class UserController : AppControllerBase
    {
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

        [HttpPut(Router.UserRouting.Edite)]
        public async Task<IActionResult> Edite([FromBody] EditeUserCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }

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
