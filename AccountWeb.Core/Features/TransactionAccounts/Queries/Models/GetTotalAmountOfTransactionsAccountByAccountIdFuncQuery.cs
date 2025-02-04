using AccountWeb.Core.Bases;
using MediatR;

namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Models
{
    public class GetTotalAmountOfTransactionsAccountByAccountIdFuncQuery : IRequest<Response<decimal>>
    {
        public int AccountId { get; set; }
    }
}
