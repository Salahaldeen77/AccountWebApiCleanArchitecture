using AccountWeb.Core.Bases;
using AccountWeb.Data.Responses;
using MediatR;

namespace AccountWeb.Core.Features.TransactionAccounts.Queries.Models
{
    public class OperationsTransferAmountToThisAccountIdQuery : IRequest<Response<List<OperationsTransferAmountToThisAccountIdQueryResponse>>>
    {
        public int AccountId { get; set; }
    }
}
