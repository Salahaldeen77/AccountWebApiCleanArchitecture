using AutoMapper;

namespace AccountWeb.Core.Mapping.AccountMap
{
    public partial class AccountProfile : Profile
    {
        public AccountProfile()
        {
            GetAccountListMapping();
            GetAccounByIdMapping();
            AddAccountCommandMapping();
            EditAccountCommandMapping();
        }
    }
}
