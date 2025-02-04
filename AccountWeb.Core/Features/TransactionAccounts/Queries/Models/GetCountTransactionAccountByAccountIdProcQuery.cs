using AccountWeb.Core.Bases;
using AccountWeb.Core.Features.TransactionAccounts.Queries.Responses;
using MediatR;

namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Models
{
    public class GetCountTransactionAccountByAccountIdProcQuery : IRequest<Response<List<GetCountTransactionAccountByAccountIdProcResponse>>>
    {
        public int AccountId { get; set; }
    }
}
