using AccountWeb.Api.Base;
using AccountWeb.Core.Features.Users.Commands.Models;
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
    }
}
