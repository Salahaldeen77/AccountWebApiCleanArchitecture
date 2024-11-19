using AccountWeb.Core.Features.Accounts.Queries.Responses;
using AccountWeb.Data.Entities;

//namespace AccountWeb.Core.Mapping.Accounts.QueryMapping
namespace AccountWeb.Core.Mapping.AccountMap
{
    public partial class AccountProfile
    {
        public void GetAccountListMapping()
        {
            CreateMap<Account, GetAccountListResponse>()
                .ForMember(dest => dest.Balance, opt => opt.MapFrom(src => src.OpeningBalance));
        }
    }
}
