using AccountWeb.Core.Features.TransactionAccounts.Queries.Responses;
using AccountWeb.Data.Entities.Views;

namespace AccountWeb.Core.Mapping.TransactionAccountMap

{
    public partial class TransactionAccountProfile
    {
        public void GetViewTransactionAccountListMapping()
        {
            CreateMap<ViewTransAccounts, GetViewTransactionAccountListResponse>();
        }
    }
}
