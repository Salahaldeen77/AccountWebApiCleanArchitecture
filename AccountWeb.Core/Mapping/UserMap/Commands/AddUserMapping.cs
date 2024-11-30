using AccountWeb.Core.Features.Users.Commands.Models;
using AccountWeb.Data.Entities.Identity;

namespace AccountWeb.Core.Mapping.UserMap
{
    public partial class UserProfile
    {
        public void AddUserMapping()
        {
            CreateMap<AddUserCommand, User>();
        }
    }
}

