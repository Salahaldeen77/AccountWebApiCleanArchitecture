using AccountWeb.Api.Base;
using AccountWeb.Core.Features.TransactionAccounts.Commands.Models;
using AccountWeb.Core.Features.TransactionAccounts.Queries.Models;
using AccountWeb.Core.Filters;
using AccountWeb.Data.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AccountWeb.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")] //Access to the class private except User
    [ServiceFilter(typeof(AuthFilter))] //Use my filter
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

        [HttpGet(Router.TransactionAccountRouting.GetViewTransactionAccountList)]
        public async Task<IActionResult> GetViewTransactionAccountList()
        {
            var response = await _mediator.Send(new GetViewTransactionAccountListQuery());
            return Ok(response);
        }
        [HttpGet(Router.TransactionAccountRouting.GetCountTransactionAccountByAccountIdProcs)]
        public async Task<IActionResult> GetCountTransactionAccountByAccountIdProcs([FromQuery] int AccountId)
        {
            var response = await _mediator.Send(new GetCountTransactionAccountByAccountIdProcQuery() { AccountId = AccountId });
            return Ok(response);
        }
        [HttpGet(Router.TransactionAccountRouting.GetTotalAmountOfTransactionsAccountByAccountIdFunc)]
        public async Task<IActionResult> GetTotalAmountOfTransactionsAccountByAccountIdFunc([FromQuery] int AccountId)
        {
            var response = await _mediator.Send(new GetTotalAmountOfTransactionsAccountByAccountIdFuncQuery() { AccountId = AccountId });
            return Ok(response);
        }
        [HttpGet(Router.TransactionAccountRouting.GetOperationsTransferAmountToThisAccountId)]
        public async Task<IActionResult> GetOperationsTransferAmountToThisAccountId([FromQuery] int AccountId)
        {
            var response = await _mediator.Send(new OperationsTransferAmountToThisAccountIdQuery() { AccountId = AccountId });
            return Ok(response);
        }
        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
