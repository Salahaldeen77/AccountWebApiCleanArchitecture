using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities;
using AccountWeb.Service.Abstracts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.Accounts.Commands.Handlers
{
    public class TransactionAccountCommandHandler : ResponseHandler, IRequestHandler<AddTransactionAccountCommand, Response<string>>
    {

        #region Fields
        private readonly ITransactionAccountService _transactionAccountService;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public TransactionAccountCommandHandler(ITransactionAccountService transactionAccountService,
                                                IAccountService accountService,
                                                IMapper mapper,
                                                IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            this._transactionAccountService = transactionAccountService;
            this._mapper = mapper;
            _accountService = accountService;
        }
        #endregion

        #region Handel Functions
        public async Task<Response<string>> Handle(AddTransactionAccountCommand request, CancellationToken cancellationToken)
        {

            //Mapping Between request and TransactionAccount
            var transactionAccountMapper = _mapper.Map<TransactionAccount>(request);
            //check validation
            if (transactionAccountMapper.TransactionId == 1)//is payment
            {
                transactionAccountMapper.IsDebit = true;
                transactionAccountMapper.TransferredToAccountId = null;
                if (await _transactionAccountService.PaymentAccountAsync(transactionAccountMapper.AccountId, transactionAccountMapper.Amount) == "Success" &&
                    await _transactionAccountService.AddTransactionAccountAsync(transactionAccountMapper) == "Success")
                {
                    return Created("Note:TransactionId = /1-Pyment\\2-Receipt\\3-Transfer\\     You Chose 1-Pyment");
                }
                else return BadRequest<string>("Account is Not Found");
            }
            else if (transactionAccountMapper.TransactionId == 2)//is Receipt
            {
                transactionAccountMapper.TransferredToAccountId = null;
                transactionAccountMapper.IsDebit = false;
                if (await _transactionAccountService.ReceiptAccountAsync(transactionAccountMapper.AccountId, transactionAccountMapper.Amount) == "Success" &&
                    await _transactionAccountService.AddTransactionAccountAsync(transactionAccountMapper) == "Success")
                {
                    return Created("Note:TransactionId = 1-Pyment\\2-Receipt\\3-Transfer\\     You Chose 2-receipt");
                }
                else
                {
                    Account account = await _accountService.GetAccountByIdAsNoTrackingAsync(transactionAccountMapper.AccountId);
                    return BadRequest<string>($"Receipt Amount is More Than Account Balance ={account.OpeningBalance}",
                                                "Note:TransactionId = 1-Pyment\\2-Receipt\\3-Transfer\\    You Chose 2-receipt");
                }
            }
            else if (transactionAccountMapper.TransactionId == 3)//is transfer
            {
                transactionAccountMapper.IsDebit = false;
                if (await _transactionAccountService.TransferAccountAsync(transactionAccountMapper) == "Success" &&
                    await _transactionAccountService.AddTransactionAccountAsync(transactionAccountMapper) == "Success")
                {
                    return Created("Note:TransactionId = /1-Pyment\\2-Receipt\\3-Transfer\\     You Chose 3-Transfer");
                }
                else return BadRequest<string>("One The Account is Not Found or transfer Amount is More Than Account Balance",
                                                "Note:TransactionId = /1-Pyment\\2-Receipt\\3-Transfer\\     You Chose 3-Transfer");
            }

            else return BadRequest<string>();

        }
        #endregion

    }
}
