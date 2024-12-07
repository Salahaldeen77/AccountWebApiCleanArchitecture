using AccountWeb.Api.Base;
using AccountWeb.Core.Features.Authentication.Commands.Models;
using AccountWeb.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace AccountWeb.Api.Controllers
{
    [ApiController]
    public class AuthenticationController : AppControllerBase
    {
        [HttpPost(Router.Authentication.SignIn)]
        public async Task<IActionResult> Create([FromForm] SignInCommand command)
        {
            var response = await Mediator.Send(command);
            return NewResult(response);
        }
    }
}
