using AccountWeb.Core.Features.Accounts.Commands.Models;
using AccountWeb.Data.Entities;

//namespace AccountWeb.Core.Mapping.AccountMap.CommandMapping
namespace AccountWeb.Core.Mapping.AccountMap

{
    public partial class AccountProfile
    {
        public void EditAccountCommandMapping()
        {
            CreateMap<EditAccountCommand, Account>()
                .ForMember(dest => dest.OpeningBalance, opt => opt.MapFrom(src => src.Balance))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        }
    }



}
