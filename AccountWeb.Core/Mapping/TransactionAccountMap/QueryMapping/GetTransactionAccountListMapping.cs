using AccountWeb.Core.Features.Accounts.Queries.Responses;
using AccountWeb.Data.Entities;

//namespace AccountWeb.Core.Mapping.TransactionAccounts.QueryMapping
namespace AccountWeb.Core.Mapping.TransactionAccountMap

{
    public partial class TransactionAccountProfile
    {
        public void GetTransactionAccountListMapping()
        {
            CreateMap<TransactionAccount, GetTransactionAccountListResponse>()
                .ForMember(dest => dest.AccounNumber, opt => opt.MapFrom(src => src.Account.AccountNumber))
                .ForMember(dest => dest.AccountName, opt => opt.MapFrom(src => src.Account.Name))
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Transaction.Type));
        }
    }
}
