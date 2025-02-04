using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.TransactionAccounts.Commands.Models;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities;
using AccountWeb.Service.Abstracts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.TransactionAccounts.Commands.Handlers
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
            _transactionAccountService = transactionAccountService;
            _mapper = mapper;
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
                var payemntResult = await _transactionAccountService.PaymentAccountAsync(transactionAccountMapper);
                if (payemntResult == "Success")
                    return Created("Add Payment is successfull");
                else
                    return BadRequest<string>(payemntResult, "Note:TransactionId = /1-Pyment\\2-Receipt\\3-Transfer\\     You Chose 1-Payment");
            }//
            else if (transactionAccountMapper.TransactionId == 2)//is Receipt
            {
                transactionAccountMapper.TransferredToAccountId = null;
                transactionAccountMapper.IsDebit = false;
                var receiptResult = await _transactionAccountService.ReceiptAccountAsync(transactionAccountMapper);
                if (receiptResult == "Success")
                    return Created("Add Receipt is successfull");
                else
                    return BadRequest<string>(receiptResult, "Note:TransactionId = /1-Pyment\\2-Receipt\\3-Transfer\\     You Chose 2-Receipt");

            }
            else if (transactionAccountMapper.TransactionId == 3)//is transfer
            {
                transactionAccountMapper.IsDebit = false;
                var transferResult = await _transactionAccountService.TransferAccountAsync(transactionAccountMapper);
                if (transferResult == "Success")
                    return Created("Add Transfer is successfull");

                else
                    return BadRequest<string>(transferResult, "Note:TransactionId = /1-Pyment\\2-Receipt\\3-Transfer\\     You Chose 3-Transfer");
            }

            else return BadRequest<string>();

        }
        #endregion

    }
}
