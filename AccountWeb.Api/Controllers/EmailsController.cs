using AccountWeb.Api.Base;
using AccountWeb.Core.Features.Emails.Commands.Models;
using AccountWeb.Data.AppMetaData;
using Microsoft.AspNetCore.Mvc;

namespace AccountWeb.Api.Controllers
{
    [ApiController]
    public class EmailsController : AppControllerBase
    {
        [HttpPost(Router.EmailsRouting.SendEmail)]
        public async Task<IActionResult> Create([FromQuery] SendEmailCommand command)
        {
            return NewResult(await Mediator.Send(command));
        }
    }
}
