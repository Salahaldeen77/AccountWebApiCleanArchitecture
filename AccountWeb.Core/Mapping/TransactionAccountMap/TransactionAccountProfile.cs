using AutoMapper;

namespace AccountWeb.Core.Mapping.TransactionAccountMap
{
    public partial class TransactionAccountProfile : Profile
    {
        public TransactionAccountProfile()
        {
            GetTransactionAccountListMapping();
            AddTransactionAccountCommandMapping();
            GetViewTransactionAccountListMapping();
            GetCountTransactionAccountByAccountIdProcMapping();
        }
    }
}
