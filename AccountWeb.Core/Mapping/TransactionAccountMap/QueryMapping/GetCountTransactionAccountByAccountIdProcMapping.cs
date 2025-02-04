using AccountWeb.Core.Features.TransactionAccounts.Queries.Responses;
using AccountWeb.Data.Entities.Procedures;

namespace AccountWeb.Core.Mapping.TransactionAccountMap

{
    public partial class TransactionAccountProfile
    {
        public void GetCountTransactionAccountByAccountIdProcMapping()
        {
            CreateMap<CountTransactionAccountByAccountIdProc, GetCountTransactionAccountByAccountIdProcResponse>();
        }
    }
}
