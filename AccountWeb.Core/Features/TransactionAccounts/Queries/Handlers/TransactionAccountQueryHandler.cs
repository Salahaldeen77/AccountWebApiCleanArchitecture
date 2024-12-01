using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.TransactionAccounts.Queries.Models;
using AccountWeb.Core.Features.TransactionAccounts.Queries.Responses;
using AccountWeb.Core.Resources;
using AccountWeb.Service.Abstracts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;

namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Handlers
{
    public class TransactionAccountQueryHandler : ResponseHandler, IRequestHandler<GetTransactionAccountListResponseQuery, Response<List<GetTransactionAccountListResponse>>>
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
    }


}
