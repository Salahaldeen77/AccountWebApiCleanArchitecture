using AccountWeb.Api.Base;
using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Core.Features.Accounts.Queries.Models;
using AccountWeb.Core.Filters;
using AccountWeb.Data.AppMetaData;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountWeb.Api.Controllers
{
    //[Route("api/[controller]")]

    [ApiController]
    [Authorize(Roles = "Admin,User")] //Access to the Class Private Except Admin or User
    [ServiceFilter(typeof(AuthFilter))] //Use my filter
    public class AccountController : AppControllerBase
    {

        //[HttpGet("/Account/List")]
        [HttpGet(Router.AccountRouting.List)]
        public async Task<IActionResult> GetAccountList()
        {
            var response = await Mediator.Send(new GetAccountListResponseQuery());
            return Ok(response);
        }
        [HttpGet(Router.AccountRouting.Paginated)]
        public async Task<IActionResult> Paginated([FromQuery] GetAccountPaginatedListQuery query)
        {
            var response = await Mediator.Send(query);
            return Ok(response);
        }
        // [HttpGet("/Account/{Id}")]
        [HttpGet(Router.AccountRouting.GetById)]
        public async Task<IActionResult> GetAccountById([FromRoute] int Id)
        {
            //var response =await Mediator.Send(new GetAccountByIdQuery(Id));
            //return Ok(response);

            //New way to shorten Code
            return NewResult(await Mediator.Send(new GetAccountByIdQuery(Id)));
        }

        [Authorize(Policy = "CreateAccount")]
        [HttpPost(Router.AccountRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddAccountCommand command)
        {
            //var response =await Mediator.Send(command);
            //return Ok(response);

            return NewResult(await Mediator.Send(command));
        }

        [Authorize(Policy = "EditAccount")]
        [HttpPut(Router.AccountRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditAccountCommand command)
        {
            //var response =await Mediator.Send(command);
            //return Ok(response);

            return NewResult(await Mediator.Send(command));
        }

        [Authorize(Policy = "DeleteAccount")]
        [HttpDelete(Router.AccountRouting.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            //var response =await Mediator.Send(command);
            //return Ok(response);
            return NewResult(await Mediator.Send(new DeleteAccountCommand(Id)));
        }
    }
}
