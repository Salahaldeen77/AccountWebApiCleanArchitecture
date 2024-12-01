using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.TransactionAccounts.Queries.Responses;
using AccountWeb.Data.Entities;
using MediatR;

namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Models
{
    public class GetTransactionAccountListQuery : IRequest<Response<List<TransactionAccount>>>
    {

    }
    public class GetTransactionAccountListResponseQuery : IRequest<Response<List<GetTransactionAccountListResponse>>>
    {

    }


}
