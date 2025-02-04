using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.TransactionAccounts.Queries.Models;
using AccountWeb.Core.Features.TransactionAccounts.Queries.Responses;
using AccountWeb.Core.Resources;
using AccountWeb.Data.Entities.Procedures;
using AccountWeb.Data.Responses;
using AccountWeb.Service.Abstracts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Handlers
{
    public class TransactionAccountQueryHandler : ResponseHandler, IRequestHandler<GetTransactionAccountListResponseQuery, Response<List<GetTransactionAccountListResponse>>>,
                                                                   IRequestHandler<GetViewTransactionAccountListQuery, Response<List<GetViewTransactionAccountListResponse>>>,
                                                                   IRequestHandler<GetCountTransactionAccountByAccountIdProcQuery, Response<List<GetCountTransactionAccountByAccountIdProcResponse>>>,
                                                                   IRequestHandler<GetTotalAmountOfTransactionsAccountByAccountIdFuncQuery, Response<decimal>>,
                                                                   IRequestHandler<OperationsTransferAmountToThisAccountIdQuery, Response<List<OperationsTransferAmountToThisAccountIdQueryResponse>>>
    {
        #region Fileds
        private readonly ITransactionAccountService _transactionAccountService;
        private readonly IMapper _mapper;
        #endregion
        #region Constructors
        public TransactionAccountQueryHandler(ITransactionAccountService transactionAccount,
                                                IMapper mapper,
                                                IStringLocalizer<SharedResources> localizer) : base(localizer)
        {
            _transactionAccountService = transactionAccount;
            _mapper = mapper;
        }
        #endregion
        public async Task<Response<List<GetTransactionAccountListResponse>>> Handle(GetTransactionAccountListResponseQuery request, CancellationToken cancellationToken)
        {
            var transactionAccountList = await _transactionAccountService.GetAllTransactionAccountWithIncludeAsync();
            var transactionAccountListMapper = _mapper.Map<List<GetTransactionAccountListResponse>>(transactionAccountList);
            return Success(transactionAccountListMapper);
        }

        public async Task<Response<List<GetViewTransactionAccountListResponse>>> Handle(GetViewTransactionAccountListQuery request, CancellationToken cancellationToken)
        {
            var viewTransAccount = await _transactionAccountService.GetViewTransAccountsAsync();
            var result = _mapper.Map<List<GetViewTransactionAccountListResponse>>(viewTransAccount);
            return Success(result);
        }

        public async Task<Response<List<GetCountTransactionAccountByAccountIdProcResponse>>> Handle(GetCountTransactionAccountByAccountIdProcQuery request, CancellationToken cancellationToken)
        {

            var result = await _transactionAccountService.GetCountTransactionAccountByAccountIdProcsAsync(new CountTransactionAccountByAccountIdProcParameters() { AccountId = request.AccountId });
            var mapp = _mapper.Map<List<GetCountTransactionAccountByAccountIdProcResponse>>(result);
            return Success(mapp);
        }

        public async Task<Response<decimal>> Handle(GetTotalAmountOfTransactionsAccountByAccountIdFuncQuery request, CancellationToken cancellationToken)
        {
            var result = await _transactionAccountService.GetTotalAmountOfTransactionsAccountByAccountIdFuncAsync(request.AccountId);
            return Success(result);
        }

        public async Task<Response<List<OperationsTransferAmountToThisAccountIdQueryResponse>>> Handle(OperationsTransferAmountToThisAccountIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _transactionAccountService.GetOperationsTransferAmountToThisAccountIdAsync(request.AccountId);
            return Success(result);
        }
    }


}
