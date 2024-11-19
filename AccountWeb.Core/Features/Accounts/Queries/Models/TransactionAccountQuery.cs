using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.Accounts.Queries.Responses;
using AccountWeb.Data.Entities;
using MediatR;

namespace AccountWeb.Core.Features.Accounts.Queries.Models
{
    public class GetTransactionAccountListQuery : IRequest<Response<List<TransactionAccount>>>
    {

    }
    public class GetTransactionAccountListResponseQuery : IRequest<Response<List<GetTransactionAccountListResponse>>>
    {

    }


}
