using AccountWeb.Core.Features.Accounts.Queries.Responses;
using AccountWeb.Data.Entities;

//namespace AccountWeb.Core.Mapping.Accounts.QueryMapping
namespace AccountWeb.Core.Mapping.AccountMap
{
    public partial class AccountProfile
    {
        public void GetAccounByIdMapping()
        {
            CreateMap<Account, GetSingleAccountResponse>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.OpeningBalance))
                .ForMember(dest => dest.TransactionAccounts, opt => opt.MapFrom(src => src.TransactionAccounts));

            CreateMap<TransactionAccount, TransactionAccountRespone>()
                .ForMember(dest => dest.TransactionType, opt => opt.MapFrom(src => src.Transaction.Type))
                .ForMember(dest => dest.TransactionAccountId, opt => opt.MapFrom(src => src.Id));
        }
    }
}
