using AccountWeb.Core.Features.Users.Queries.Responses;
using AccountWeb.Data.Entities.Identity;

namespace AccountWeb.Core.Mapping.UserMap
{
    public partial class UserProfile
    {
        public void GetUserByIdMapping()
        {
            CreateMap<User, GetUserByIdResponse>();

        }
    }
}
