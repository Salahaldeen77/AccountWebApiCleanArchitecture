using AccountWeb.Api.Base;
using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Core.Features.Accounts.Queries.Models;
using AccountWeb.Data.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace AccountWeb.Api.Controllers
{
    [ApiController]
    public class TransactionAccountController : AppControllerBase
    {
        

        private readonly IMediator _mediator;

        public TransactionAccountController(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpGet(Router.TransactionAccountRouting.List)]
        public async Task<IActionResult> GetTransactionAccountList()
        {
            var response = await _mediator.Send(new GetTransactionAccountListResponseQuery());
            return Ok(response);
        }
        [HttpPost(Router.TransactionAccountRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddTransactionAccountCommand command)
        {
            //var response =await Mediator.Send(command);
            //return Ok(response);

            return NewResult(await Mediator.Send(command));
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
