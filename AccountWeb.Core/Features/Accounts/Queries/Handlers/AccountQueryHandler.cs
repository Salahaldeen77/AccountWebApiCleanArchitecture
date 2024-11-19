using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Accounts.Queries.Models;
using AccountWeb.Core.Features.Accounts.Queries.Responses;
using AccountWeb.Core.Resources;
using AccountWeb.Core.Wrappers;
using AccountWeb.Data.Entities;
using AccountWeb.Service.Abstracts;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Linq.Expressions;

namespace AccountWeb.Core.Features.Accounts.Queries.Handlers
{
    public class AccountQueryHandler : ResponseHandler,
                                    IRequestHandler<GetAccountListResponseQuery, Response<List<GetAccountListResponse>>>,
                                    IRequestHandler<GetAccountByIdQuery, Response<GetSingleAccountResponse>>,
                                    IRequestHandler<GetAccountPaginatedListQuery, PaginatedResult<GetAccountPaginatedListListResponse>>
    {

        #region Fileds
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        #endregion

        #region Constructors
        public AccountQueryHandler(IAccountService accountService,
                                   IMapper mapper,
                                   IStringLocalizer<SharedResources> Localizer) : base(Localizer)
        {
            _accountService = accountService;
            _mapper = mapper;
        }
        #endregion

        #region Handle Functions
        public async Task<Response<List<GetAccountListResponse>>> Handle(GetAccountListResponseQuery request, CancellationToken cancellationToken)
        {
            //return (List<Account>)await _accountService.FindAllAsync();
            var AccountList = await _accountService.GetAllAccountsWithIncludeAsync();
            var AccountListMapper = _mapper.Map<List<GetAccountListResponse>>(AccountList);
            var result = Success(AccountListMapper);
            result.Meta = new { CountAccounts = AccountListMapper.Count };
            return result;
        }

        public async Task<Response<GetSingleAccountResponse>> Handle(GetAccountByIdQuery request, CancellationToken cancellationToken)
        {
            var account = await _accountService.GetAccountByIdWithIncludeAsync(request.Id);

            if (account == null)
                //return NotFound<GetSingleAccountResponse>("Account Not Found");
                return NotFound<GetSingleAccountResponse>();

            var result = _mapper.Map<GetSingleAccountResponse>(account);
            return Success(result);
        }

        public async Task<PaginatedResult<GetAccountPaginatedListListResponse>> Handle(GetAccountPaginatedListQuery request, CancellationToken cancellationToken)
        {
            Expression<Func<Account, GetAccountPaginatedListListResponse>> expression = e => new GetAccountPaginatedListListResponse(e.Id, e.AccountNumber, e.Name, e.OpeningBalance, e.IsActive, e.TransactionAccounts.Count);
            //var querable = _accountService.GetAllAccountsQuerable();
            var FilterQuery = _accountService.FilterAccountPaginatedQuerable(request.Search, request.OrderBy);
            var PaginatedList = await FilterQuery.Select(expression).ToPaginatedListAsync(request.PageNumber, request.PageSize);
            PaginatedList.Meta = new { CountAccounts = PaginatedList.Data.Count };
            return PaginatedList;
        }
        #endregion


    }
}
