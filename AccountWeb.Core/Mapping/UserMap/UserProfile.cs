using AutoMapper;

namespace AccountWeb.Core.Mapping.UserMap
{
    public partial class UserProfile : Profile
    {
        public UserProfile()
        {
            AddUserMapping();
            GetUserPaginationMapping();
            GetUserByIdMapping();
        }

    }
}
